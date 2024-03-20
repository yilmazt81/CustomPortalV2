using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public partial class FormVersion
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string FormLanguage { get; set; }
        public string FileName { get; set; }

        public virtual FormDefination FormDefination { get; set; }
    }
}
