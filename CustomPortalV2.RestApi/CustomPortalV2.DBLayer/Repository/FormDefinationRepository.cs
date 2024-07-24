using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FormDefinationRepository : IFormDefinationRepository
    {
        DBContext _dbContext;
        public FormDefinationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FormDefination> Get(Expression<Func<FormDefination, bool>> predicate)
        {
          
            return _dbContext.FormDefination.Where(predicate).ToList();
        }
    }
}
