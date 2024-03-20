using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FormDefination
{
    public class CustomeField
    {
        public CustomeField()
        {
            this.CustomeField_VirtualTable = new HashSet<CustomeField_VirtualTable>();
            this.CustomeFieldItem = new HashSet<CustomeFieldItem>();
        }

        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldTagName { get; set; }
        public string ElementType { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> HeaderHeightRuleValue { get; set; }
        public Nullable<int> RowHeightRuleValue { get; set; }
        public Nullable<int> RowHeight { get; set; }
        public Nullable<int> HeaderHeight { get; set; } 
        public virtual ICollection<CustomeField_VirtualTable> CustomeField_VirtualTable { get; set; } 
        public virtual ICollection<CustomeFieldItem> CustomeFieldItem { get; set; }

    }
}
