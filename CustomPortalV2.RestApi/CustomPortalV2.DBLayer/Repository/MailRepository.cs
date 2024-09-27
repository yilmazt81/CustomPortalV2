using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Log;
using CustomPortalV2.Core.Model.Setting;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class MailRepository : IMailRepository
    {
        DBContext _dbContext;
        public MailRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddFormSendMailLog(FormSendMailLog formSendMailLog)
        {
            _dbContext.FormSendMailLogs.Add(formSendMailLog);
            _dbContext.SaveChanges();
        }

        public void AddMailToCompany(int mainCompanyId, int mainBranchId, string mailAdress)
        {
            if (!_dbContext.CompanyMailLists.Any(s => s.CompanyBranchId == mainBranchId && s.MailAdress == mailAdress))
            {
                _dbContext.CompanyMailLists.Add(new CompanyMailList()
                {
                    CompanyBranchId = mainBranchId,
                    MailAdress = mailAdress,
                    MainCompanyId = mainCompanyId,
                });
            }
            _dbContext.SaveChanges();
        }

        public string[] GetCompanyBranchMail(int mainCompanyId, int branchId)
        {
            var branch = _dbContext.Branches.Single(s => s.Id == branchId);
            var query = _dbContext.CompanyMailLists.Where(s => s.MainCompanyId == mainCompanyId).AsQueryable();

            if (branch.CompanyAdmin)
            {
                query = query.Where(s => s.CompanyBranchId == branch.Id);
            }

            return query.ToArray().Select(s => s.MailAdress).ToArray();

        }

        public CompanySmtpSetting? GetCompanySmtpSetting(int mainCompanyId)
        {
            return _dbContext.CompanySmtpSettings.FirstOrDefault(s => s.MainCompanyId == mainCompanyId);
        }
    }
}
