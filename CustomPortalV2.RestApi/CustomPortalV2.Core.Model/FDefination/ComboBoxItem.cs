using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public string TagName { get; set; }
        public int MainCompanyId { get; set; }

    }
}
