using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Autocomplete
{
    public partial class AutoComplateDefination
    {
        public int Id { get; set; }
        public string DefinationType { get; set; }
        public string SearchParam { get; set; }
        public string TextField { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field4 { get; set; }
        public string Field3 { get; set; }
    }
}
