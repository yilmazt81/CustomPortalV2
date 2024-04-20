using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class CustomeField_VirtualTableCalculateRow
    {
        public int Id { get; set; }
        public int VirtualTableId { get; set; }
        public int VirtualColumnId { get; set; }
        public string CalculateFormula { get; set; }
    }
}
