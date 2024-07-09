using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FormMetaDataService : IFormMetaDataService
    {
        public DefaultReturn<List<FormMetaData>> FilterForms(FormMetaDataFilterPost formMetaData)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormMetaData>> GetCompanyFormMetaData(User user)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormMetaData> GetFormMetaData(User user, int id)
        {
            throw new NotImplementedException();
        }
    }
}
