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
        public Nullable<int> CurrentFlowStepId { get; set; }
        public Nullable<int> NextFlowStepId { get; set; }
        public bool Complated { get; set; }
        public Nullable<int> ComplatedUserId { get; set; }
        public string ComplatedUser { get; set; }
        public Nullable<System.DateTime> ComplatedDate { get; set; }
        public Nullable<System.DateTime> LastStepDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> MainCompanyId { get; set; }
        public Nullable<int> CompanyBranchId { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public string WorkFlowName { get; set; }

        public virtual CustomWork CustomWork { get; set; } 
        public virtual ICollection<CustomWorkWorkFlowDocument> CustomWorkWorkFlowDocument { get; set; } 
        public virtual ICollection<CustomWorkWorkFlowStep> CustomWorkWorkFlowStep { get; set; }
    }
}
