using CustomPortalV2.Core.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IBranchRepository
    {

        List<Branch> GetBranches(Expression<Func<Branch, bool>> predicate);
        Branch? Get(Expression<Func<Branch, bool>> predicate);
        Branch AddBranch(Branch branch);
    }
}
