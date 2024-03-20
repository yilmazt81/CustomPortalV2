using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class FormAttachmentType
    {
          public FormAttachmentType()
        {
            this.FormDefinationAttachment = new HashSet<FormDefinationAttachment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
         
        public virtual ICollection<FormDefinationAttachment> FormDefinationAttachment { get; set; }
    }
}
