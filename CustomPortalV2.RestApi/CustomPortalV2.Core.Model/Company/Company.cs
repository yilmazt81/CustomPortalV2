using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Company
{
    public class Company
    {

        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string TaxNumber { get; set; }

        public string TaxOffice { get; set; }

        public string AuthorizedPersonName { get; set; }

        public int? CountryId { get; set; }


        public int? CityId { get; set; }

        public string MersisNo { get; set; }

    }
}
