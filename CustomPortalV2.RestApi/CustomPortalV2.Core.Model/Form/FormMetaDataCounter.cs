using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Form
{
    public partial class FormMetaDataCounter
    {
        public int Id { get; set; }
        public System.DateTime? CounterDate { get; set; }
        public int CompanyBranchId { get; set; }
        public int? FormCount { get; set; }
    }
}
