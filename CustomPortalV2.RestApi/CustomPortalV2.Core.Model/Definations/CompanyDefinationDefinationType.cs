using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Definations
{
    public class CompanyDefinationDefinationType
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CompanyDefinationId { get; set; }
        [JsonPropertyName("typeid")]
        public int DefinationTypeId { get; set; }
        [JsonIgnore]
        public virtual CompanyDefination CompanyDefination { get; set; }
    }
}
