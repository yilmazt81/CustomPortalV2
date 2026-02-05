using CustomPortalV2.Core.Model.Log;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class LogRepository : ILogRepository
    {
        DBContext _dbContext;

        public LogRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddCreateSoftDocumentLog(UserCreateSoftDocumentLog userCreateSoftDocumentLog)
        {
            _dbContext.UserCreateSoftDocumentLog.Add(userCreateSoftDocumentLog);
            _dbContext.SaveChanges();
        }
    }
}
