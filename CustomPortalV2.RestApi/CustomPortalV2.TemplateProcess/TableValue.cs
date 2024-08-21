using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.TemplateProcess
{
    public class TableValue
    {
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }


        public string FieldValue { get; set; }
        public int? FontSize { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
    }
}
