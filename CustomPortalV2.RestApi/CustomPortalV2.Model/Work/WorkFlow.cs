using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Work
{
    public class WorkFlow
    {
        public int Id { get; set; }
        public string WorkFlowName { get; set; }

        public int MainCompanyId { get; set; }
        public string CreatedBy { get; set; }

        public string CreatedId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? EditedId { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }
    }
}
