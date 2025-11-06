using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.DTO
{
    public class DefaultReturn<T>:BaseReturn
    {
        public DefaultReturn()
        {
            ReturnCode = 1;
            ReturnMessage = "ok";
        }

        public DefaultReturn(int code, string message)
        {
            ReturnCode = code;
            ReturnMessage = message;
        }

        public void SetException(Exception exception)
        {
            ReturnCode = 5;
            ReturnMessage = exception.Message + "  " + (exception.InnerException != null ? exception.InnerException.Message : "");
        }
        public T Data { get; set; }
    }
}
