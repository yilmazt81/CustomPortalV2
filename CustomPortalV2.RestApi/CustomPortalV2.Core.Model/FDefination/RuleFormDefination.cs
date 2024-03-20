using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class RuleFormDefination
    {
        public int Id { get; set; }
        public int FormRuleId { get; set; }
        public int FormDefinationId { get; set; }
        public string FormName { get; set; }
        public Nullable<int> FormDefinationFieldId { get; set; }
        public string TagName { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeElseValue { get; set; }

        public virtual FormDefination FormDefination { get; set; }
        public virtual FormRule FormRule { get; set; }
    }
}
