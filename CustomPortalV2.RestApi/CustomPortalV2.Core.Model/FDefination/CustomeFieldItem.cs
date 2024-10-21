using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class CustomeFieldItem
    {
        public int Id { get; set; }
        public int CustomeFieldId { get; set; }
        public  bool  Mandatory { get; set; }
        public string FieldCaption { get; set; }
        public string ControlType { get; set; }
        public string TagName { get; set; }
        public bool AutoComplate { get; set; }
        public bool Deleted { get; set; }
        public Nullable<int> MaxCharacter { get; set; }
        public string CellName { get; set; }
        public Nullable<int> TableWith { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<int> HeaderWidthRuleValue { get; set; }
        public string HeaderWidth { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<bool> Bold { get; set; }
        public Nullable<bool> Italic { get; set; }

        public int? HeaderHeightRuleValue { get; set; }

        [JsonIgnore]
        public virtual CustomeField CustomeField { get; set; }
        public int? HeaderHeight { get; set; }
        public int? RowHeightRuleValue { get; set; }
        public int? RowHeight { get; set; }
        public int MainCompanyId { get; set; }
    }
}
