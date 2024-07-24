using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class CustomSector
    {
        public int Id { get; set; }
        public int MainCompanyId { get; set; }
        public string Name { get; set; }

        public int BaseCustomeSectorId { get; set; }

    }
}
