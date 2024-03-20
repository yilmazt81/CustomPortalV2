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
        public int MainCompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string PhoneNumber   { get; set; }

        public bool WaitForAllowApp { get; set; }

        public int BranchPackageId { get; set; }

        public string BranchPackageName { get; set; }

        public bool Deleted { get; set; }
        public int UserRuleId { get; set; }

        public string UserRuleName { get; set; }
         
        public bool CompanyAdmin { get; set; }

        public bool SysAdmin { get; set; }


    }
}
