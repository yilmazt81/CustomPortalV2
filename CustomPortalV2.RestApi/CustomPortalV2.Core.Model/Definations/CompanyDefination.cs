using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Definations
{
    public class CompanyDefination
    {
        public CompanyDefination()
        {
            this.CompanyDefinationDefinationType = new HashSet<CompanyDefinationDefinationType>();
        }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string IsoCode { get; set; }
        public bool IsSender { get; set; }
        public bool Deleted { get; set; }
        public string DefinationTypeId { get; set; }
        public string DefinationTypeName { get; set; }
        public  int  MainCompanyId { get; set; }
        public  int  CompanyBranchId { get; set; }
        public int? CountryId { get; set; }
        public int? CountryCityId { get; set; }
        public string    FieldForSearch { get; set; }
        public string FactoryNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }


        [JsonProperty("definations")]
        public virtual ICollection<CompanyDefinationDefinationType> CompanyDefinationDefinationType { get; set; }
    }
}
