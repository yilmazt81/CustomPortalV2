using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class MailService : IMailService
    {
        IMailRepository _mailRepository;
        IBranchRepository _branchRepository;
        public MailService(IMailRepository mailRepository, IBranchRepository branchRepository)
        {
            _mailRepository = mailRepository;
            _branchRepository = branchRepository;
        }
        public DefaultReturn<string[]> GetCompanyMails(int mainCompanyId, int companyBranchId)
        {
            DefaultReturn<string[]> defaultReturn = new DefaultReturn<string[]>();

            defaultReturn.Data = _mailRepository.GetCompanyBranchMail(mainCompanyId, companyBranchId);
            return defaultReturn;
        }

        public DefaultReturn<bool> SendMail(int mainCompanyId, int mainCompanyBranchId, int userId, AttachmentSendMailDTO attachmentSendMailDTO)
        {
            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();

            try
            {
                var smtpSetting = _mailRepository.GetCompanySmtpSetting(mainCompanyId);
                var companyBranch = _branchRepository.Get(s => s.Id == mainCompanyBranchId);

                if (smtpSetting == null)
                {
                    throw new Exception("Mail ayarlarınız yapılmamış");
                }

                if (string.IsNullOrEmpty(companyBranch.Email))
                {
                    throw new Exception("Şubenin Email adresi girilmemiş");

                }

                Encryption encryption = new Encryption("TS_299989f4-2f");

                var mailPassword = encryption.Decrypt(companyBranch.EMailPassword);

                var credentials = new NetworkCredential(companyBranch.Email, mailPassword);


                var mailMessage = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress(companyBranch.Email, companyBranch.Name),
                    Subject = attachmentSendMailDTO.Subject,
                };
                if (!string.IsNullOrEmpty(attachmentSendMailDTO.CCMails))
                {
                    foreach (var item in attachmentSendMailDTO.CCMails.Split(';'))
                    {
                        if (string.IsNullOrEmpty(item))
                            continue;

                        mailMessage.CC.Add(item);
                        _mailRepository.AddMailToCompany(mainCompanyId, mainCompanyBranchId, item);
                    }
                }

                if (!string.IsNullOrEmpty(attachmentSendMailDTO.ToMails))
                {
                    foreach (var item in attachmentSendMailDTO.ToMails.Split(';'))
                    {
                        if (string.IsNullOrEmpty(item))
                            continue;

                        mailMessage.To.Add(item);
                        _mailRepository.AddMailToCompany(mainCompanyId, mainCompanyBranchId, item);
                    }
                }

                using (SmtpClient smtpClient = new SmtpClient(smtpSetting.SmtpServer, smtpSetting.SmtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = credentials;
                    smtpClient.Send(mailMessage);
                }

                _mailRepository.AddFormSendMailLog(new Core.Model.Log.FormSendMailLog() { AppUserId = userId, AttachmentCount = attachmentSendMailDTO.FormAttachmentList.Length, CompanySmtpSettingId = smtpSetting.Id, MailBody = attachmentSendMailDTO.Body, CreatedDate = DateTime.Now, MailHeader = attachmentSendMailDTO.Subject, ToMail = attachmentSendMailDTO.ToMails });

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }


            return defaultReturn;
        }
    }
}
