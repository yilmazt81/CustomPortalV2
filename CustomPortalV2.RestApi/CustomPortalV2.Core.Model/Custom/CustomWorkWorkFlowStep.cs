using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Custom
{
    public class CustomWorkWorkFlowStep
    {
        public int Id { get; set; }
        public int CustomWorkWorkFlowId { get; set; }
        public int? AppUserId { get; set; }
        public int? CompanyBranchId { get; set; }
        public string Name { get; set; }
        public int? FlowStepId { get; set; }
        public byte? StepStateId { get; set; }
        public string UserNote { get; set; }
        public System.DateTime? EditedDate { get; set; }
        public string EditedUser { get; set; }
        public int? EditedUserId { get; set; }

        public virtual CustomWorkWorkFlow CustomWorkWorkFlow { get; set; }
    }
}
