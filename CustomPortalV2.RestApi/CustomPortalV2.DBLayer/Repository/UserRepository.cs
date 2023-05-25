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
        public User GetUserName(string userName, int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
