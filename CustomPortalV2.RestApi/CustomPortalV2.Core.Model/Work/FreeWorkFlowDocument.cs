using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Work
{
    public class FreeWorkFlowDocument
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string CreatedDate { get; set; }

        public byte StateId { get; set; }

        public string ClientIp { get; set; }

        public string Source { get; set; }


    }
}
