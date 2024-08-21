using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormMetaDataAttachmentFilter
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public string FilterValue { get; set; }

        public int FormDefinationAttachmentId { get; set; }

        public int FormDefinationId { get; set; }

        public int CreateCount { get; set; }


    }
}
