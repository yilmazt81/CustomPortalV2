using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class UserRuleMenuDTO
    {


        public string name { get; set; }
        public string to { get; set; }

        public string icon { get; set; }

        public badge badge { get; set; }

        public string color { get; set; }
        public UserRuleMenuItem items { get; set; }
    }
    public class badge  {
        public string color { get; set; }
        public string text { get; set; }
    }
    public class UserRuleMenuItem
    {
        public string name { get; set; }
        public string to { get; set; }

        public string icon { get; set; }

    }
}
