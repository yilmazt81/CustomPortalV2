using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Model.Custom
{
    public class CustomWork
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }

        public int CompanyBranchId { get; set; }
        public int CreatedId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? EditedId { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public string CustomSectorId { get; set; }

        public string WorkName { get; set; }


        public bool Delete { get; set; }

    }
}
