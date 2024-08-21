using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.TemplateProcess
{
    public static class Helper
    {
        public static string ToUpperTrk(this string value)
        {
            if (value == null)
                return string.Empty;
            value = value.ToUpper();
            value = value.Replace("İ", "I");
            value = value.Replace("Ş", "S");
            value = value.Replace("Ç", "C");
            value = value.Replace("Ö", "O");
            value = value.Replace("Ğ", "G");
            value = value.Replace("Ü", "U");

            return value;
        }
    }
}
