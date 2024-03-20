using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class CreateCompanyRequest
    {

        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxNumber { get; set; }

        public string MersisNo { get; set; }

        public string AuthorizedPersonName { get; set; }

        public int Country { get; set; }
        public int City { get; set; }

        public string Adress { get; set; }

        public string Password { get; set; }
        public string PasswordRetry { get; set; }




    }
}
