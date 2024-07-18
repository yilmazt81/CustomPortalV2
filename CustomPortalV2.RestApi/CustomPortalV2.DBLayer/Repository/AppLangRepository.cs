using CustomPortalV2.Core.Model.Lang;
using CustomPortalV2.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class AppLangRepository : IAppLangRepository
    {

        DBContext _dbContext;
        public AppLangRepository(DBContext dbContext) {
            _dbContext = dbContext;
        }

        public string Get(string appTag, int languageId, string defaultValue)
        {
            var appLangTranslate = _dbContext.ApplicationLanguage.FirstOrDefault(s => s.LangId == languageId && s.TranslateTag == appTag);

            return (appLangTranslate == null ? defaultValue : appLangTranslate.TranslateText);
        }

        public List<AppLang> GetAppLangs()
        {
            return _dbContext.AppLangs.ToList();
        }

    }
}
