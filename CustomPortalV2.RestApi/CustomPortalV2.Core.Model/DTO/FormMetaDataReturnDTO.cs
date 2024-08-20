using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class FormMetaDataReturnDTO
    {
        public FormMetaDataReturnDTO(FormMetaData formMetaData)
        {
            
        }
        public bool UseTemplate { get; set; }
    }
}
