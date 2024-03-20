using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormRule
    {
        public FormRule()
        {
            this.AppUser = new HashSet<User>();
            this.RuleFormDefination = new HashSet<RuleFormDefination>();
        }

        public int Id { get; set; }
        public string RuleName { get; set; }
        public Nullable<int> FormDefinationFieldId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public Nullable<int> FormDefinationId { get; set; }
        public string CheckBoxValue { get; set; }
         
        public virtual ICollection<User> AppUser { get; set; } 
        public virtual ICollection<RuleFormDefination> RuleFormDefination { get; set; }
    }
}
