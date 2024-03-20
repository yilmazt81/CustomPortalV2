using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Form
{
    public partial class SoftDocumentShare
    {
        public int Id { get; set; }
        public int FormMetaDataId { get; set; }
        public int FormVersionId { get; set; }
        public string ShareUser { get; set; }
        public int ShareUserId { get; set; }
        public Nullable<System.DateTime> ShareDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Token { get; set; }
        public string FormLanguage { get; set; }
        public Nullable<int> DownloadCount { get; set; }
    }
}
