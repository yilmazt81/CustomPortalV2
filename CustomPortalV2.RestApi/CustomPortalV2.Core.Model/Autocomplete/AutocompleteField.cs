using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Autocomplete
{
    public partial class AutocompleteField
    {
        public int Id { get; set; }
        public int FormDefinationFieldId { get; set; }
        public string FieldName { get; set; }
        public string ComplateObject { get; set; }
        public string FilterValue { get; set; }
        public string RelationalFieldName { get; set; }
    }
}
