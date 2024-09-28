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

        public bool DeleteBranch(Branch branch)
        {
            var deletedB = _dbContext.Branches.Single(s => s.Id == branch.Id);
            deletedB.Deleted = true;
            _dbContext.SaveChanges();
            return true;
        }

        public Branch? Get(Expression<Func<Branch, bool>> predicate)
        {
            return _dbContext.Branches.FirstOrDefault(predicate);
        }

        public List<Branch> GetBranches(Expression<Func<Branch, bool>> predicate)
        {

            return _dbContext.Branches.Where(predicate).ToList();
        }

        public Branch UpdateBrach(Branch branch)
        {
            var oldBrach = _dbContext.Branches.Single(s => s.Id == branch.Id);

            oldBrach.BranchPackageId = branch.BranchPackageId;
            oldBrach.Name = branch.Name;
            oldBrach.BranchPackageName = branch.BranchPackageName;
            oldBrach.PhoneNumber = branch.PhoneNumber;
            oldBrach.Email = branch.Email;
            oldBrach.EMailPassword = branch.EMailPassword;
            oldBrach.UserRuleId = branch.UserRuleId;
            oldBrach.UserRuleName = branch.UserRuleName;
            _dbContext.SaveChanges();

            return oldBrach;
        }
    }
}
