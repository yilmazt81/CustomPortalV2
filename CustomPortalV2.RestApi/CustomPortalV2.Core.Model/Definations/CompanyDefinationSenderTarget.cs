using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Definations
{
    public class CompanyDefinationSenderTarget
    {
        public int Id { get; set; }
        public int SenderCompanyId { get; set; }
        public int TargetCompanyId { get; set; }
        public Nullable<int> MainCompanyId { get; set; }
    }
}
