using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        DBContext _dbContext;
        public UserRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AddUser(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public User? Get(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll(int mainCompany)
        {

            return _dbContext.Users.Where(s => s.MainCompanyId == mainCompany && !s.Deleted).ToList();
        }

        public IEnumerable<BranchPackage> GetBranchPackages()
        {
           return _dbContext.BranchPackage.ToList();
        }

        public IEnumerable<User> GetBranchUsers(int branchId)
        {
            return _dbContext.Users.Where(s => s.CompanyBranchId == branchId && !s.Deleted).ToList();
        }

        public User? GetUserName(string userName, int companyId)
        {
            return _dbContext.Users.FirstOrDefault(s => s.MainCompanyId == companyId && s.UserName == userName);

        }

        public IEnumerable<UserRuleMenu> GetUserRuleMenus(int userRuleId)
        {
            return _dbContext.UserRuleMenu.Where(s => s.UserRuleId == userRuleId).ToArray();
        }

        public IEnumerable<UserRule> GetUserRules()
        {

            return _dbContext.UserRule.ToList();
        }

        public bool Update(User user)
        {
            var dbUser = _dbContext.Users.Single(s => s.Id == user.Id);
            if (!string.IsNullOrEmpty(user.Password))
            {
                dbUser.Password = user.Password;
            }
            dbUser.FullName = user.FullName;
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.CompanyBranchId = user.CompanyBranchId;
            dbUser.Deleted=user.Deleted;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
