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
        public FormConvertContainerDTO() {
            AttachmentPrivateForForm = new List<FormDefinationAttachment>();
            AttachmentNotPrivateForForm = new List<FormDefinationAttachment>();
        }

        public string FormDefinationTypeName { get; set; }
        public FormVersion[] FormVersions { get; set; }

        public List<FormDefinationAttachment > AttachmentPrivateForForm { get; set; }
        public List<FormDefinationAttachment>  AttachmentNotPrivateForForm { get; set; }
    }
}
