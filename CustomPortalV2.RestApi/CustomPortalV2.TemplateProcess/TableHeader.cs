using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.TemplateProcess
{
    public class TableHeader
    {
        public string Caption { get; set; }
        public string With { get; set; }
        public int? Height { get; set; }
        public int? HeaderWithRule { get; set; }
        public int? HeaderHeightRule { get; set; }

        public int? RowHeightRule { get; set; }

        public int? RowHeightValue { get; set; }

        public string FieldName { get; set; }


        public int? FontSize { get; set; }
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
    }
}
