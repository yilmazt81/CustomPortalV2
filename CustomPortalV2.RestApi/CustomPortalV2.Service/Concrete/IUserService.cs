using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomPortalV2.Business.Helper.Enums;

namespace CustomPortalV2.Business.Concrete
{
    public interface IUserService
    {
        enumLoginReturn Login(string clientIp, string companyCode, string username, string password, ref User user);
        DefaultReturn<User> UpdateUser(User user);
        DefaultReturn<List<User>> GetUsers(int companyId, int branchId);
        DefaultReturn<User> GetById(int companyId, int id);
        User GetUserByUserName(string companyCode, string userName);

        //bool DeleteUser(User user);
        DefaultReturn<bool> DeleteUser(int companyId, int branchId, int id);

        DefaultReturn<List<UserRuleMenuDTO>> GetUserManu(int userId, int branchId);

        DefaultReturn<List<UserRule>> GetUserRoles(int companyId, int userId);

        DefaultReturn<List<BranchPackage>> GetBranchPackage(int companyId, int userId);

        DefaultReturn<User> AddUser(User user);

    }
}
