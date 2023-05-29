using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Company
{
    public class CountryCity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
       // public string IsoCode { get; set; }

    }
}
