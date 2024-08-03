using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFormMetaDataService
    {

        DefaultReturn<List<FormMetaData>> GetBranchFormMetaData(int companyId,int brachId);
        DefaultReturn<FormMetaData> GetCompanyFormMetaData(int companyId,int branchId,int id);

        DefaultReturn<FormMetaData> GetFormMetaData(User user,int id);

        DefaultReturn<List<FormMetaData>> FilterForms(FormMetaDataFilterPost formMetaData);


    }
}
