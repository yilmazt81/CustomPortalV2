using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class FormGroup
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string Name { get; set; }
        public string FormNumber { get; set; }
        public int? OrderNumber { get; set; }
        public bool?AllowEditCustomer { get; set; }
        public string GroupTag { get; set; }

        public bool Deleted { get; set; }

    }
}
