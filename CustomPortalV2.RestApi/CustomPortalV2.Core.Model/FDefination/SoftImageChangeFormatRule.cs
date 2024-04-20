using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class SoftImageChangeFormatRule
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string StartTag { get; set; }
        public string EndTag { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Operation { get; set; }

        public virtual FormDefination FormDefination { get; set; }
    }
}
