using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Lang;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class AppLangService : IAppLangService
    {
        IAppLangRepository _appLangRepository;

        public AppLangService(IAppLangRepository appLangRepository)
        {
            _appLangRepository=appLangRepository;
        }
        public List<AppLang> GetAppLangs()
        {
           
            return _appLangRepository.GetAppLangs();
        }
    }
}
