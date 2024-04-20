using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Custom
{
    public class CustomWorkWorkFlowDocument
    {
        public int Id { get; set; }
        public int CustomWorkWorkFlowId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int CustomWorkDocumentId { get; set; }
        public string DocumentName { get; set; }
        public string FileExtention { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public string OpenUser { get; set; }
        public Nullable<bool> IsOpenForm { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public string EditUser { get; set; }
        public Nullable<bool> IsEditForm { get; set; }

        public virtual CustomWorkWorkFlow CustomWorkWorkFlow { get; set; }
    }
}
