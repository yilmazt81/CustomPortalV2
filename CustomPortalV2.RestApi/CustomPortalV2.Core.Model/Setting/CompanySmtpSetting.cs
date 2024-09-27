using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Setting
{
    public class CompanySmtpSetting
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountName { get; set; }
        public int  SmtpPort { get; set; }
        public string MailTemplate { get; set; }
        public string SmtpServer { get; set; }
        public string MailSignature { get; set; }
    }
}
