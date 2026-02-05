using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FoodPersonelRepository : IFoodPersonelRepository
    {
        DBContext _dbContext;
         
        public FoodPersonelRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FoodPersonel> GetFoodPersonel(Expression<Func<FoodPersonel, bool>> predicate)
        {
            
            return _dbContext.FoodPersonels.Where(predicate).ToArray();
        }
    }
}
