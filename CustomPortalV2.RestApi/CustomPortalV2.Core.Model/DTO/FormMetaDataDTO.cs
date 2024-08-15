using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class FormMetaDataDTO
    {
      

        public int id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }

        public int BrachId { get; set; }
        public int formDefinationTypeid { get; set; }
        public FieldValueDTO[] fieldValues { get; set; }

        public bool  isDefault { get; set; }

        public int  workid { get; set; }
    }

    public class FieldValueDTO
    {

        public string fieldName { get; set; }
        public string fieldValue { get; set; }
    }
}
