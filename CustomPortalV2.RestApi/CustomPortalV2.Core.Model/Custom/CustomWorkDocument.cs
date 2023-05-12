using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Custom
{
    public class CustomWorkDocument
    {
        public int Id { get; set; }
        public int CustomWorkId { get; set; }

        public int? FormMetaDataId { get; set; }

        public string UploadFormPath { get; set; }
        public string FormFileName { get; set; }
        public string CreatedBy { get; set; }

        public string CreatedId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FileExtention { get; set; }

        public decimal FileSize { get; set; }

        public string EditedBy { get; set; }
        public string EditedId { get; set; }

    }
}
