using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormVersionPDFFieldMap
    {
        public int Id { get; set; }
        public int FormVersionId { get; set; }
        public string PDFFieldName { get; set; }
        public string TagName { get; set; }
        public string CalculateField { get; set; }
        public int? FormDefinationId { get; set; }
        public int? FontSize { get; set; }
    }
}
