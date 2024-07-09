using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IBranchService
    {

        DefaultReturn<List<Branch>> GetCompanyBraches(int companyId);

        DefaultReturn<Branch> GetBranch(int id);

        DefaultReturn<Branch> AddBranch(Branch branch);
    }
}
