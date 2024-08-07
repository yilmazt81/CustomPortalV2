using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class ObjectFieldDTO
    {
        public ObjectFieldDTO() { }

        public ObjectFieldDTO(string name,string caption) {
            Name = name;
            Caption = caption;
        }
        public string Caption { get; set; }

        public string Name { get; set; }
        
    }
}
