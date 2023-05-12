using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPortalV2.Model
{
    public class ReturnType<T>
    {
        public ReturnType()
        {
            ReturnCode = 1;
            ReturnMessage = "ok";
        }

        public void SetException(Exception exception)
        {
            ReturnCode = 5;
            ReturnMessage = exception.Message +"  "+ (exception.InnerException != null ? exception.InnerException.Message : "");
        }
        public int ReturnCode { get; set; }

        public string ReturnMessage { get; set; }

        public T SingleValue { get; set; }

        public List<T> MultiValue { get; set; }
    }
}
