using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class ParamRepository: IParamRepository
    {

        DBContext _dbContext;
        public ParamRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddParam(Param param)
        {
            _dbContext.Add(param);
        }

        public Param? GetParam(string name)
        {
            return _dbContext.Parameters.FirstOrDefault(p => p.Name == name);
        }
    }
}
