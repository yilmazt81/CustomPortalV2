using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Log
{
    public class CopyDocumentLog
    {
        public int Id { get; set; }
        public int FormMetaDataId { get; set; }
        public int NewMetaDataId { get; set; }
        public  System.DateTime? CreateDate { get; set; }
        public int CopyUserId { get; set; }
        public string CopyUser { get; set; }
        public int MainCompanyId { get; set; }
        public int CompanyBranchId { get; set; }
    }
}
