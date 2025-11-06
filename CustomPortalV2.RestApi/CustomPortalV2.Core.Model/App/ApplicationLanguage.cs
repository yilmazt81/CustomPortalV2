using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.App
{
    public class ApplicationLanguage
    {
        public int Id { get; set; }
        public string TranslateTag { get; set; }
        public byte? LangId { get; set; }
        public string TranslateText { get; set; }
        public System.DateTime? CreatedDate { get; set; }

        public byte? MustTranslate { get; set; }
    }
}
