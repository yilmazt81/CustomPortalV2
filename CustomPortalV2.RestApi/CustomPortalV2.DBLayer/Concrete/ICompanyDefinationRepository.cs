using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ICompanyAdresDefinationRepository
    {
        IEnumerable<DefinationType> GetDefinationTypes();

        CompanyDefination Add(CompanyDefination companyDefination);

        CompanyDefination Update(CompanyDefination companyDefination);

        List<CompanyDefination> GetCompanyDefinations(Expression<Func<CompanyDefination, bool>> predicate);

    }
}
