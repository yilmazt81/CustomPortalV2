using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormDefinationAttachment
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string FileName { get; set; }
        public string FormName { get; set; } 
        public string FormAttachmentTypeName { get; set; }
        public  int  FontSize { get; set; }
        public  bool  Bold { get; set; }
        public  bool  Italic { get; set; }
        [JsonIgnore]
        public virtual FormAttachmentType? FormAttachmentType { get; set; }
        [JsonIgnore]
        public virtual FormDefination? FormDefination { get; set; }
    }
}
