using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Company
{
    public class Country
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderNumber { get; set; }

        public string IsoCode { get; set; }

        public bool? ForSaleProduct { get; set; }
    }
}
