using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Work
{
    public class WorkFlowEdge
    {

        public int Id { get; set; }

        public int WorkFlowId { get; set; }

        public string EdgeType { get; set; }

        public int Source { get; set; }

        public int Target { get; set; }

        public bool Animated { get; set; }

    }
}
