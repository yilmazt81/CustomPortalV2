using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class BaseReturn
    {
        public int ReturnCode { get; set; }

        public string ReturnMessage { get; set; }
    }
}
