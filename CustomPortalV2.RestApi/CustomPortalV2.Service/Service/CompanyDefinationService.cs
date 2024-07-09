using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CompanyDefinationService : ICompanyDefinationService
    {
        public DefaultReturn<bool> DeleteCompany(User user, int id)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<CompanyDefination>> GetCompanyDefinations(User user)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<CompanyDefinationDefinationType>> GetDefinationTypes()
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<CompanyDefination> Save(CompanyDefination companyDefination)
        {
            throw new NotImplementedException();
        }
    }
}
