using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Helper
{
    public class Enums
    {

        public enum enumLoginReturn
        {
            CompanyCodeIsNotExist,
            CompanyDisabled,
            UserNameOrPasswordWrong,
            UserDisable,
            FailTimeOutMinute,
            LoginFailMaxCount,
            Success,
        }
    }
}
