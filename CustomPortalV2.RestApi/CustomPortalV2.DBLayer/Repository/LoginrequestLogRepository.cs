using CustomPortalV2.Core.Model.App;
using CustomPortalV2.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class LoginrequestLogRepository : ILoginrequestLogRepository
    {
        DBContext _dbContext;
        public LoginrequestLogRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddLoginLog(LoginRequestLog loginRequestLog)
        {
            _dbContext.LoginRequestLogs.Add(loginRequestLog);
            _dbContext.SaveChanges();
        }

        public int CheckFailedLoginCount(string clientIp, DateTime minimumDate)
        {
            return _dbContext.LoginRequestLogs.Count(s => s.ClientIp == clientIp && !s.Success && s.LogDate > minimumDate);
        }
    }
}
