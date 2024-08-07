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
        public  int FormDefinationFieldId { get; set; }
        public string TagName { get; set; }
        public string FieldCaption { get; set; }
        public string PropertyValue1 { get; set; }
        public  int  FormGroupId { get; set; }
        //public string TagName2 { get; set; }
        public string PropertyValue2 { get; set; }
        public string PropertyValue3 { get; set; }
    }
}
