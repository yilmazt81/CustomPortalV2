using Newtonsoft.Json;
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
        public int? FormAttachmentTypeId { get; set; }
        public int? FormDefinationId { get; set; }
        public int CustomeFieldId { get; set; }
        public int? TableBorder { get; set; }
        public string ElementType { get; set; }
        public  bool Deleted { get; set; }
        public string FormDefinationName { get; set; }
        public string FormAttachmentName { get; set; }
        public int? FormDefinationAttachmentId { get; set; }
        public string FormDefinationAttachmentName { get; set; }
        public int? HeaderHeightRuleValue { get; set; }
        public int? RowHeightRuleValue { get; set; }
        public int? RowHeight { get; set; }
        public int? HeaderHeight { get; set; }
        public int? FontSize { get; set; }
        public  bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public int? FormVersionId { get; set; }

        public int MainCompanyId { get; set; }
        [JsonIgnore]
        public virtual CustomeField CustomeField { get; set; } 
        public virtual ICollection<CustomeField_VirtualTableField> CustomeField_VirtualTableField { get; set; }
    }
}
