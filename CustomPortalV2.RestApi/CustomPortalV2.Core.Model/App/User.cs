using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public class User
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }

        public int CompanyBranchId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int FormRuleId { get; set; }
        public string BranchName { get; set; }

    }
}
