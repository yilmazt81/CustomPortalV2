using CustomPortalV2.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Custom
{
    public class CustomWorkPermission
    {
        public int Id { get; set; }
        public int CustomWorkId { get; set; }
        public int AppUserId { get; set; }
        public string UserFullName { get; set; }
        public bool? Admin { get; set; }
        public long? ReadOnly { get; set; }
        public System.DateTime? CreatedDate { get; set; }

        public virtual CustomWork CustomWork { get; set; }
    }
}
