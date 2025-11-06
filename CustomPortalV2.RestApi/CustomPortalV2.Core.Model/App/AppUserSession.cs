using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public class AppUserSession
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public  System.Guid? Token { get; set; }
        public System.DateTime? StartDate { get; set; }
        public System.DateTime? EndDate { get; set; }

    }
}
