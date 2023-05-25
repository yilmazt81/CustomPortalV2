
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class LoginReturn: BaseReturn
    {
        public LoginReturn()
        {
            ReturnCode = 1;
            ReturnMessage = "ok";
        }
        public string token { get; set; }
        public bool IsLogin { get; set; }

        public int UserId { get; set; }
    }
}
