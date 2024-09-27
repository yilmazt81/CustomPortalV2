using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IMailService
    {

        DefaultReturn<string[]> GetCompanyMails(int mainCompanyId, int companyBranchId);

        DefaultReturn<bool> SendMail(int mainCompanyId, int mainCompanyBranchId,int userId, AttachmentSendMailDTO attachmentSendMailDTO);


    }
}
