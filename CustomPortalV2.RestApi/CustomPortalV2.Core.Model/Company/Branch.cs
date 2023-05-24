using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Company
{
    public class Branch
    {

        public int Id { get; set; }

        public string Name { get; set; }


        public CustomPortalV2.Model.Company.Company Company { get; set; }
    }
}
