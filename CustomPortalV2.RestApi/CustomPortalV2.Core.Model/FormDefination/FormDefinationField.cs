using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FormDefination
{
    public class FormDefinationField
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string ControlType { get; set; }
        public Nullable<bool> Mandatory { get; set; }
        public string FieldCaption { get; set; }
        public string TagName { get; set; }
        public Nullable<int> FormGroupId { get; set; }
        public Nullable<bool> AutoComplate { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string CellName { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<bool> Bold { get; set; }
        public Nullable<bool> Italic { get; set; }
        public string DefaultProp { get; set; }
        public string TranslateLanguage { get; set; }

        public virtual FormDefination FormDefination { get; set; }
        public virtual FormGroup FormGroup { get; set; } 
    }
}
