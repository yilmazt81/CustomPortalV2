using CustomPortalV2.Core.Model.Log;
using CustomPortalV2.Core.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IMailRepository
    {

        string[] GetCompanyBranchMail(int mainCompanyId, int branchId);

        CompanySmtpSetting? GetCompanySmtpSetting(int mainCompanyId);

        void AddMailToCompany(int mainCompanyId, int mainBranchId, string mailAdress);

        void AddFormSendMailLog(FormSendMailLog formSendMailLog);

    }
}
