using CustomPortalV2.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business
{
    public interface IUserService
    {
        bool Login(string username, string password);
        bool UpdateUser(User user);
        List<User> GetUsers(int companyId);
        User GetById(int id);
        User AddUser(User user);
        bool DeleteUser(User user);
    }
    public class UserService : IUserService
    {
        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(int companyId)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        
    }


}
