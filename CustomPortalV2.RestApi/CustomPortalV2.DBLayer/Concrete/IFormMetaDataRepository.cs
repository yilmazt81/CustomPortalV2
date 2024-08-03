using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IFormMetaDataRepository
    {
        IEnumerable<FormMetaData> Get(Expression<Func<FormMetaData, bool>> predicate,int maxCount);
    }
}
