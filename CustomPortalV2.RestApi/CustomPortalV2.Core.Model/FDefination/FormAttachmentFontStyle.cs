using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormAttachmentFontStyle
    {
        public int Id { get; set; }
        public int FormDefinationAttachmentId { get; set; }
        public string TagName { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public int FontSize { get; set; }
        public string FieldCaption { get; set; }
        public bool CustomeField { get; set; }
        public string TranslateLanguage { get; set; }

        public string FontFamily { get; set; }

    }
}
