using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Work
{
    public class WorkFlowNode
    {
        public WorkFlowNode() { }

        public int Id { get; set; }

        public int WorkFlowId { get; set; }

        public string NodeType { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

    }
}
