using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Autocomplete
{
    public partial class AutoComplateOldValue
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldValueUpper { get; set; }
        public System.DateTime LastUseDate { get; set; }
        public bool? CustomeField { get; set; }
    }
}
