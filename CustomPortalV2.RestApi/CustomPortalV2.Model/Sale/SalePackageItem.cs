using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Sale
{
    public class SalePackageItem
    {
        public int Id { get; set; }
        public int SalePackageId { get; set; }
        public int SaleProductId { get; set; }
        public bool AllowUse { get; set; } 
        public string LimitCount { get; set; }
        public string CustomeText { get; set; }
    }
}
