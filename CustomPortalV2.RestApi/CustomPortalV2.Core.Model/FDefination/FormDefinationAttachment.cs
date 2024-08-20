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
        public  int  FontSize { get; set; }
        public  bool  Bold { get; set; }
        public  bool  Italic { get; set; }

        public string FontFamily { get; set; }
          

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedId { get; set; }


        public string? EditedBy { get; set; }
         
        public DateTime? EditedDate { get; set; }
         
        public int? EditedId { get; set; }
        public string FilePath { get; set; }

        public bool Active { get; set; }

   
        [JsonIgnore]
        public virtual FormDefination? FormDefination { get; set; }
    }
}
