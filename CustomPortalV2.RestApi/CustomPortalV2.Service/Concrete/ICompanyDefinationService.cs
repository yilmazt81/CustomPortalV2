using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface ICompanyDefinationService
    {
         

        DefaultReturn<List<CompanyDefination>> GetCompanyDefinations(int companyId,int brachId);

        DefaultReturn<CompanyDefination> GetCompanyDefination(int companyId, int brachId, int id);

        DefaultReturn<bool> DeleteCompany(int mainCompanyId, int id);


        DefaultReturn<CompanyDefination> Save(CompanyDefination companyDefination);

        DefaultReturn<List<DefinationType>> GetDefinationTypes();

    }
}
