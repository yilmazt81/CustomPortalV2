using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public partial class UserRule
    {
         public UserRule()
        {
            this.UserRuleMenu = new HashSet<UserRuleMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DefaultFormRuleId { get; set; }
         
        public virtual ICollection<UserRuleMenu> UserRuleMenu { get; set; }
    }
}
