using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IParamService
    {

        Param GetParam(string name);

    }
}
