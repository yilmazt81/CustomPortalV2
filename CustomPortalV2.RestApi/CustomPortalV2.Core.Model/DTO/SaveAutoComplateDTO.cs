using CustomPortalV2.Core.Model.Autocomplete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class SaveAutoComplateDTO
    {
        public AutocompleteField Complate { get; set; }

        public AutocompleteFieldMap Map { get; set; }
    }
}
