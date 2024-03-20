using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormDefinationAttachment
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string FileName { get; set; }
        public string FormName { get; set; }
        public Nullable<int> FormAttachmentTypeId { get; set; }
        public string FormAttachmentTypeName { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<bool> Bold { get; set; }
        public Nullable<bool> Italic { get; set; }

        public virtual FormAttachmentType FormAttachmentType { get; set; }
        public virtual FormDefination FormDefination { get; set; }
    }
}
