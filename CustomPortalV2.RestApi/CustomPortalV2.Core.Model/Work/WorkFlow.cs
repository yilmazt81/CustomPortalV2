using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.Core.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Work
{
    public class WorkFlow
    {

        public WorkFlow() {

            this.WorkFlowNodes = new HashSet<WorkFlowNode>();
            this.WorkFlowEdges = new HashSet<WorkFlowEdge>();
        }   
        public int Id { get; set; }
        public string WorkFlowName { get; set; }

        public int MainCompanyId { get; set; }
        public string CreatedBy { get; set; }

        public int CreatedId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? EditedId { get; set; }

        public string? EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }
        public int CompanyBranchId { get; set; }

        public string FlowType { get; set; }


        public virtual ICollection<WorkFlowNode>? WorkFlowNodes { get; set; }
        public virtual ICollection<WorkFlowEdge>? WorkFlowEdges { get; set; }
    }
}
