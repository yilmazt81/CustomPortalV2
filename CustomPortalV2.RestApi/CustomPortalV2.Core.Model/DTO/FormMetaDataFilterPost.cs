using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class FormMetaDataFilterPost
    {
        public int SectorId { get; set; }
        public int FormDefinationTypeId { get; set; }

        public string CompanyName { get; set; }
        public string SortType { get; set; }
    }
}
