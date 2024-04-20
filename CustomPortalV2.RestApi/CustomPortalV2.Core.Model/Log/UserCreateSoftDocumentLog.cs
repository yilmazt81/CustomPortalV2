using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Log
{
    public partial class UserCreateSoftDocumentLog
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public Nullable<int> FormVersionId { get; set; }
        public Nullable<int> FormAttachmentTypeId { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<int> AppUserId { get; set; }
        public int BranchId { get; set; }
    }
}
