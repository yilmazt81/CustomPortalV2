using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPortalV2.Core.Model.DTO
{
    public class CloneFormGroupRequest
    {

        public int customSectorId { get; set; }

        public int formDefinationTypeId { get; set; }

        public int targetFormDefinationId { get; set; }

        public int[] FormGroupList { get; set; }

    }
}
