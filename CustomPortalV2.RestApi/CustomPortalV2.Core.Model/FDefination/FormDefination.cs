using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormDefination
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public string FormName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedId { get; set; }
        public int CustomSectorId { get; set; }
        public string CustomSectorName { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> DesingTemplate { get; set; }
        public string TemplatePath { get; set; }

       
    }
}
