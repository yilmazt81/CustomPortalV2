using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ICustomProductRepository
    {
        IEnumerable<CustomProduct> GetCustomProducts(Expression<Func<CustomProduct, bool>> predicate);
        CustomProduct Get(int id);

        CustomProduct Add(CustomProduct customProduct);
        CustomProduct Update(CustomProduct customProduct);


    }
}
