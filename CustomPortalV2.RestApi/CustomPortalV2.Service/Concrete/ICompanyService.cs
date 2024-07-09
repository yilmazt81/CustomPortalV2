using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface ICompanyService
    { 
        DefaultReturn<CreateCompanyReturn> AddCompany(Company company, string password);
        bool IsExistCompany(Company company);

        Branch AddBrach(Branch branch);


    }
}
