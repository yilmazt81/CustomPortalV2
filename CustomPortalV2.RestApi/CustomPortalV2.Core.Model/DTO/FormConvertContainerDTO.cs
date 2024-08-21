using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class FormConvertContainerDTO
    {

        public FormVersion[] FormVersions { get; set; }

        public FormDefinationAttachment[] AttachmentPrivateForForm { get; set; }
        public FormDefinationAttachment[] AttachmentNotPrivateForForm { get; set; }
    }
}
