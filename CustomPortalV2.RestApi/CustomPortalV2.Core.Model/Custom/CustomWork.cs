using CustomPortalV2.Core.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Custom
{
    public class CustomWork
    {
        public CustomWork()
        {
            this.CustomWorkDocument = new HashSet<CustomWorkDocument>();
            this.CustomWorkWorkFlow = new HashSet<CustomWorkWorkFlow>();
            this.CustomWorkPermission = new HashSet<CustomWorkPermission>();
        }

        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? EditedId { get; set; }
        public string EditedBy { get; set; }
        public System.DateTime? EditedDate { get; set; }
        public int? CustomSectorId { get; set; }
        public string WorkName { get; set; }
        public bool?Deleted { get; set; }
        public string RecrivedCompanyName { get; set; }
        public int? RecrivedCompanyId { get; set; }
        public string SenderCompanyName { get; set; }
        public int? SenderCompanyId { get; set; }
        public System.DateTime? ClosedDate { get; set; }
        public string ClosedBy { get; set; }
        public int? ClosedId { get; set; }
        public int? WorkStateId { get; set; }
        public bool?ShareWithMainCompany { get; set; }
         
        public virtual ICollection<CustomWorkDocument> CustomWorkDocument { get; set; } 
        public virtual ICollection<CustomWorkWorkFlow> CustomWorkWorkFlow { get; set; } 
        public virtual ICollection<CustomWorkPermission> CustomWorkPermission { get; set; }

    }
}
