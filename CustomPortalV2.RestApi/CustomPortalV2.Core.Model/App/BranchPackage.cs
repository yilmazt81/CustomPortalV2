using CustomPortalV2.Core.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public class BranchPackage
    {
        public BranchPackage()
        {
            this.CompanyBranch = new HashSet<Branch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? RecordCount { get; set; }

        public int MonthlyRecordCount { get; set; }
        public virtual ICollection<Branch> CompanyBranch { get; set; }
    }
}
