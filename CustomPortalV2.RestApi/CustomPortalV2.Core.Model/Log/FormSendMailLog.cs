using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Log
{
    public class FormSendMailLog
    {
        public int Id { get; set; }
        public int CompanySmtpSettingId { get; set; }
        public string ToMail { get; set; }
        public string MailBody { get; set; }
        public int AttachmentCount { get; set; }
        public int FormMetaDataId { get; set; }
        public int AppUserId { get; set; }
        public string SendUserName { get; set; }
        public string MailHeader { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
