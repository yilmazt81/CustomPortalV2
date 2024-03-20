using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public partial class UserRuleMenu
    {
        public int Id { get; set; }
        public int UserRuleId { get; set; }
        public string MenuName { get; set; }
        public string MenuAdress { get; set; }
        public Nullable<int> AppMenuId { get; set; }
        public string IconClass { get; set; }

        public virtual UserRule UserRule { get; set; }
    }
}
