using CustomPortalV2.Core.Model.Definations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IFoodPersonelRepository
    {
        IEnumerable<FoodPersonel> GetFoodPersonel(Expression<Func<FoodPersonel, bool>> predicate);
    }
}
