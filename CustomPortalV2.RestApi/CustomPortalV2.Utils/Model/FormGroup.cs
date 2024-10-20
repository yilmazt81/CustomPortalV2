using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Utils.Model
{
    public class FormGroup
    {
        public int Id { get; set; }
        public int FormDefinationId { get; set; }
        public string Name { get; set; }
        public string FormNumber { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<bool> AllowEditCustomer { get; set; }
        public string GroupTag { get; set; }

        public bool Deleted { get; set; }

    }
}
