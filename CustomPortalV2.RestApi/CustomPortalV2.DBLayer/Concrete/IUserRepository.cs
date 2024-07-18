using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IUserRepository
    {
        User? GetUserName(string userName, int companyId);
        User? Get(int id);

        User AddUser(User user);

        bool Update(User user);

        IEnumerable<User> GetAll(int mainCompany);

        IEnumerable<UserRuleMenu> GetUserRuleMenus(int userRuleId);


    }
}
