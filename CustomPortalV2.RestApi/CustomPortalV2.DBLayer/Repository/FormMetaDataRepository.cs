using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FormMetaDataRepository : IFormMetaDataRepository
    {
        DBContext _dbContext;
        public FormMetaDataRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FormMetaData> Get(Expression<Func<FormMetaData, bool>> predicate, int maxCount)
        {
            return _dbContext.FormMetaData.Take(maxCount).Where(predicate).OrderByDescending(s => s.Id).ToList();
        }

        public FormMetaData Save(FormMetaData formMetaData)
        {
            _dbContext.FormMetaData.Add(formMetaData);
            _dbContext.SaveChanges();
            return formMetaData;
        }
    }
}
