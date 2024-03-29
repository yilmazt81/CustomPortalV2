﻿using CustomPortalV2.Core.Model.App; 
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
        enumLoginReturn Login(string clientIp, string companyCode,string username, string password,ref User? user);
        bool UpdateUser(User user);
        List<User> GetUsers(int companyId);
        User GetById(int id);
        User? GetUserByUserName(string companyCode, string userName);
        User AddUser(User user);
        bool DeleteUser(User user);
    }
}
