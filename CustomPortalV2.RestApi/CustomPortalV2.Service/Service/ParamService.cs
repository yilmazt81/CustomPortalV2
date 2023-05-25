using CustomPortalV2.Business.Concrete;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class ParamService : IParamService
    {

        IParamRepository _paramRepository;
        private static List<Param> _paramList = new List<Param>();
        public ParamService(IParamRepository paramRepository)
        {
            _paramRepository = paramRepository;
        }
        public Param GetParam(string name)
        {
            var param = _paramList.FirstOrDefault(p => p.Name == name);
            if (param == null)
            {
                param = _paramRepository.GetParam(name);
                _paramList.Add(param);

            }

            return param;
        }
    }
}
