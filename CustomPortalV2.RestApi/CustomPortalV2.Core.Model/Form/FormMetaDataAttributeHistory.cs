using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Form
{
    public partial class FormMetaDataAttributeHistory
    {
        public int Id { get; set; }
        public int FormMetaDataId { get; set; }
        public string TagName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int EditedId { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public int FormMetaDataAttributeId { get; set; }
        public Nullable<bool> ModifyByPartner { get; set; }
    }
}
