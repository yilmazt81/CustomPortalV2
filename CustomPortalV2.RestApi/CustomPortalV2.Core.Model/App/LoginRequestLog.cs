using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public class LoginRequestLog
    {

        public int Id { get; set; }
        public string UserName { get; set; }

        public DateTime LogDate { get; set; }

        public string ClientIp { get; set; }

        public bool Success { get; set; }

        public int AppUserId { get; set; }

    }
}
