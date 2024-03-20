using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ICompanyRepository
    {
        Company? GetCompanyCode(string companycode);
        Company AddCompany(Company company);

        Company? FindCompany(Expression<Func<Company, bool>> predicate);

        Branch AddBrach(Branch branch);
    }
}
