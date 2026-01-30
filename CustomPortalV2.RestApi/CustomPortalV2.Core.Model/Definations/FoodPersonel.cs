using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPortalV2.Core.Model.Definations
{
    public class FoodPersonel
    {
        public int Id { get; set; }
        public string CityName { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }



    }
}
