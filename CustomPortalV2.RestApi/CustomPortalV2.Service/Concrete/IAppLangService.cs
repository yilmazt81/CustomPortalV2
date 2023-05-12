using CustomPortalV2.Core.Model.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IAppLangService
    {
        List<AppLang> GetAppLangs();
    }
}
