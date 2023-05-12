using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.App
{
    public class UserSession
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public Guid Token { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
