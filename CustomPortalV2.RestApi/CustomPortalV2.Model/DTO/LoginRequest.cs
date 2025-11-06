using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model
{
    public class LoginRequest
    {

        public string CompanyCode { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }

        public int UserLanguage { get; set; }
    }
}
