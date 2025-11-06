using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Form
{
    public partial class FormMetaDataAttribute_CustomeField
    {
        public int Id { get; set; }
        public int FormMetaDataId { get; set; }
        public int FormDefinationFieldId { get; set; }
        public int DataOrder { get; set; }
        public string TagName { get; set; }
        public string FieldValue { get; set; }
        public  bool  Attachment { get; set; }
        [JsonIgnore]
        public virtual FormMetaData FormMetaData { get; set; }
    }
}
