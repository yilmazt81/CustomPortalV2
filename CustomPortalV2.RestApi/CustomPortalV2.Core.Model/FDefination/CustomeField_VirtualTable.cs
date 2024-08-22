using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class CustomeField_VirtualTable
    {
        public CustomeField_VirtualTable()
        {
            this.CustomeField_VirtualTableField = new HashSet<CustomeField_VirtualTableField>();
        }

        public int Id { get; set; }
        public Nullable<int> FormAttachmentTypeId { get; set; }
        public Nullable<int> FormDefinationId { get; set; }
        public int CustomeFieldId { get; set; }
        public Nullable<int> TableBorder { get; set; }
        public string ElementType { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string FormDefinationName { get; set; }
        public string FormAttachmentName { get; set; }
        public Nullable<int> FormDefinationAttachmentId { get; set; }
        public string FormDefinationAttachmentName { get; set; }
        public Nullable<int> HeaderHeightRuleValue { get; set; }
        public Nullable<int> RowHeightRuleValue { get; set; }
        public Nullable<int> RowHeight { get; set; }
        public Nullable<int> HeaderHeight { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<bool> Bold { get; set; }
        public Nullable<bool> Italic { get; set; }
        public Nullable<int> FormVersionId { get; set; }

        public int MainCompanyId { get; set; }

        public virtual CustomeField CustomeField { get; set; } 
        public virtual ICollection<CustomeField_VirtualTableField> CustomeField_VirtualTableField { get; set; }
    }
}
