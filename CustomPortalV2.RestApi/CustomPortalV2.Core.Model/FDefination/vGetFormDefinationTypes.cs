using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class vGetFormDefinationTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FormDefinationId { get; set; }
        public string FormName { get; set; }
        public int FormDefinationAttachmentId { get; set; }
    }
}
