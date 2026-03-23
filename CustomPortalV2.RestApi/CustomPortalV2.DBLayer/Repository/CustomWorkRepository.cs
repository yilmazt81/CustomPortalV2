using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CustomWorkRepository : ICustomWorkRepository
    {
        DBContext _dbContext;

        public CustomWorkRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomWork AddCustomWork(CustomWork branch)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWork(CustomWork branch)
        {
            throw new NotImplementedException();
        }

        public CustomWork? Get(Expression<Func<CustomWork, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<CustomWork> GetCustomWorks(Expression<Func<CustomWork, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CustomWork UpdateCustomWork(CustomWork branch)
        {
            throw new NotImplementedException();
        }
    }
}
