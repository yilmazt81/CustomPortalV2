using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormDefinationField
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string ControlType { get; set; }
        public bool Mandatory { get; set; }
        public string FieldCaption { get; set; }
        public string TagName { get; set; }
        public Nullable<int> FormGroupId { get; set; }
        public bool AutoComplate { get; set; }
        public  bool  Deleted { get; set; }
        public string CellName { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<int> FontSize { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public string DefaultProp { get; set; }

        public string FontFamily { get; set; }
        public string TranslateLanguage { get; set; }
        [JsonIgnore]
        public virtual FormDefination? FormDefination { get; set; }
        [JsonIgnore]
        public virtual FormGroup? FormGroup { get; set; }

        public string AutoComlateType { get; set; }

    }
}
