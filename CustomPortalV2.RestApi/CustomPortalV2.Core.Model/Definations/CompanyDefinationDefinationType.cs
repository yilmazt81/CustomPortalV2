using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.Definations
{
    public class CompanyDefinationDefinationType
    {
        public int Id { get; set; }
        public int CompanyDefinationId { get; set; }
        public int DefinationTypeId { get; set; }

        public virtual CompanyDefination CompanyDefination { get; set; }
    }
}
