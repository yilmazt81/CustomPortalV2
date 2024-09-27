using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Company
{
    public class CompanyMailList
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public string MailAdress { get; set; }
        public int CompanyBranchId { get; set; }
    }
}
