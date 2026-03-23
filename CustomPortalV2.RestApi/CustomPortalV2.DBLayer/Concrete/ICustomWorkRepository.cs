using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ICustomWorkRepository
    {
        List<CustomWork> GetCustomWorks(Expression<Func<CustomWork, bool>> predicate);
        CustomWork? Get(Expression<Func<CustomWork, bool>> predicate);
        CustomWork AddCustomWork(CustomWork branch);

        CustomWork UpdateCustomWork(CustomWork branch);

        bool DeleteWork(CustomWork branch);
    }
}
