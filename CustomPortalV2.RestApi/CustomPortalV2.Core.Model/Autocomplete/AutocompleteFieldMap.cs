using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Autocomplete
{
    public class AutocompleteFieldMap
    {
        public int Id { get; set; }
        public Nullable<int> FormDefinationFieldId { get; set; }
        public string TagName { get; set; }
        public string ProperyValue { get; set; }
        public Nullable<int> FormGroupId { get; set; }
        public string TagName2 { get; set; }
        public string ProperyValue2 { get; set; }
        public string ProperyValue3 { get; set; }
    }
}
