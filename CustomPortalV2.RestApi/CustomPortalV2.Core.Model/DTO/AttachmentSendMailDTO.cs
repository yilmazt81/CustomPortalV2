using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class AttachmentSendMailDTO
    {
        public string ToMails { get; set; }
        public string CCMails { get; set; }
        public int FormVersionId { get; set; }
        public int[] FormAttachmentList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public int FormMetaDataId { get; set; }
    }
}
