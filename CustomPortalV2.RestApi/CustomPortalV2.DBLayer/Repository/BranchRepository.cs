using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class BranchRepository : IBranchRepository
    {
        DBContext _dbContext;
        public BranchRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Branch AddBranch(Branch branch)
        {
            _dbContext.Add(branch);
            _dbContext.SaveChanges();

            return branch;
        }

        public Branch? Get(Expression<Func<Branch, bool>> predicate)
        {
            return _dbContext.Branches.FirstOrDefault(predicate);
        }

        public List<Branch> GetBranches(Expression<Func<Branch, bool>> predicate)
        {
             
            return _dbContext.Branches.Where(predicate).ToList();
        }
    }
}
