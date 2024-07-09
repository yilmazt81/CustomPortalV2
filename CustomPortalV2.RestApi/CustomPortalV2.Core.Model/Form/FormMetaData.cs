using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace CustomPortalV2.Core.Model.Form
{
    public class FormMetaData
    {
        public FormMetaData()
        {
            this.FormMetaDataAttribute = new HashSet<FormMetaDataAttribute>();
            this.FormMetaDataAttribute_CustomeField = new HashSet<FormMetaDataAttribute_CustomeField>();
        }

        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public int MainCompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public int SenderCompanyId { get; set; }
        public int RecrivedCompanyId { get; set; }
        public int CreatedId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> EditedId { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public int CustomSectorId { get; set; }
        public string FormDefinationName { get; set; }
        public string BrancName { get; set; }
        public string SenderCompanyName { get; set; }
        public string RecrivedCompanyName { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> CopyDocument { get; set; }
        public Nullable<bool> DefaultForm { get; set; }
        public Nullable<int> CustomWorkId { get; set; }

        public virtual FormDefination FormDefination { get; set; } 
        public virtual ICollection<FormMetaDataAttribute> FormMetaDataAttribute { get; set; } 
        public virtual ICollection<FormMetaDataAttribute_CustomeField> FormMetaDataAttribute_CustomeField { get; set; }
    }
}
