using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Definations
{
    public class CustomProduct
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedId { get; set; }
        public  System.DateTime  CreatedDate { get; set; }
        public int? EditedId { get; set; }
        public string? EditedBy { get; set; }
        public System.DateTime? EditedDate { get; set; }
        public int  CustomSectorId { get; set; }
        public bool Deleted { get; set; }
        public string ProductName { get; set; }
        public string ProductName_TRK { get; set; }
        public string IntendedUse { get; set; }
        public string TransferTemperature { get; set; }
        public string? CustomSectorName { get; set; }
        public string Transfercondition { get; set; }
        public string GtipCode { get; set; }
        public string ProductCulture { get; set; }

        public string ScientificName { get; set; }

    }
}
