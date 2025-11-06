using CustomPortalV2.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Custom
{
    public class CustomWorkWorkFlow
    {
        public CustomWorkWorkFlow()
        {
            this.CustomWorkWorkFlowDocument = new HashSet<CustomWorkWorkFlowDocument>();
            this.CustomWorkWorkFlowStep = new HashSet<CustomWorkWorkFlowStep>();
        }
        public int Id { get; set; }
        public int CustomWorkId { get; set; }
        public int WorkFlowId { get; set; }
        public int? CurrentFlowStepId { get; set; }
        public int? NextFlowStepId { get; set; }
        public bool Complated { get; set; }
        public int? ComplatedUserId { get; set; }
        public string ComplatedUser { get; set; }
        public System.DateTime? ComplatedDate { get; set; }
        public System.DateTime? LastStepDate { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? MainCompanyId { get; set; }
        public int? CompanyBranchId { get; set; }
        public string CreatedUser { get; set; }
        public int? CreatedUserId { get; set; }
        public string WorkFlowName { get; set; }

        public virtual CustomWork CustomWork { get; set; } 
        public virtual ICollection<CustomWorkWorkFlowDocument> CustomWorkWorkFlowDocument { get; set; } 
        public virtual ICollection<CustomWorkWorkFlowStep> CustomWorkWorkFlowStep { get; set; }
    }
}
