using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormAttachmentExcelMap
    {
        public int Id { get; set; }
        public int FormDefinationAttachmentId { get; set; }
        public string TagName { get; set; }
        public string FieldCaption { get; set; }
        public string CalculateField { get; set; }
        public string ExcelCell { get; set; }
        public string WorkSheetName { get; set; }
        public string SumCell { get; set; }
        public Nullable<int> FormGroupId { get; set; }
    }
}
