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

        DefaultReturn<List<CompanyDefinationDefinationType>> GetDefinationTypes();

        DefaultReturn<List<CompanyDefination>> GetCompanyDefinations(User user);

        DefaultReturn<bool> DeleteCompany(User user, int id);


        DefaultReturn<CompanyDefination> Save(CompanyDefination companyDefination);

    }
}
