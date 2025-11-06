using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class FormVersion
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string FormLanguage { get; set; }
        public string FileName { get; set; }

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
