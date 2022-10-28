using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Work
{
    public class WorkFlowStep
    {

        public int Id { get; set; }
        public int WorkFlowId { get; set; }
        public int AppUserId { get; set; }
        public int CompanyBranchId { get; set; }
        public int FlowStepId { get; set; }
        public string BranchName { get; set; }
        public int StepNo { get; set; }

    }
}
