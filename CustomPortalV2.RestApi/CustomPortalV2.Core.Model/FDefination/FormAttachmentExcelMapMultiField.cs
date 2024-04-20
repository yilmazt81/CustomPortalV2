using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormAttachmentExcelMapMultiField
    {
        public int Id { get; set; }
        public int FormAttachmentExcelMapId { get; set; }
        public string FieldHeader { get; set; }
        public string TagName { get; set; }
        public string StartCell { get; set; }
        public string Formula { get; set; }
    }
}
