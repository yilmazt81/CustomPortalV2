using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Helper
{
    public static class StringHelper
    {
        public static void GetExcelAdress(string cellName, ref string header, ref uint row)
        {
            header = string.Empty;
            string rowStr = "";
            for (int i = 0; i < cellName.Length; i++)
            {
                int k = 0;
                if (int.TryParse(cellName[i].ToString(), out k))
                {
                    rowStr += cellName[i];
                }
                else
                {
                    header += cellName[i];
                }
            }

            row = uint.Parse(rowStr);

        }
        public static bool IsSaveSqlInjection(this string value)
        {
            if (value == null)
                return false;
            if (value.Contains("--"))
                return true;
            if (value.ToLower().Contains("delete"))
                return true;
            if (value.ToLower().Contains("drop"))
                return true;
            if (value.ToLower().Contains("create"))
                return true;
            if (value.ToLower().Contains("update"))
                return true;
            return false;
        }

        public static string ReplaceIncorrectedCharForUrl(this string value)
        {
            return value.ToTrktoING().Replace(@"\", "_").Replace("$", "_").Replace("%", "_").Replace("?", "_").Replace(" ", "_").Replace(":", "_").Replace(".", "_");
        }

        public static bool IsDateEmpty(this string value)
        {
            var tempStr = value.Replace('_', ' ').Trim('.', ' ').Replace(',', ' ').Trim();

            return string.IsNullOrEmpty(tempStr);
        }
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
        public static string ToTrktoING(this string value)
        {
            value = value.Replace("ı", "i");
            value = value.Replace("ş", "s");
            value = value.Replace("ç", "c");
            value = value.Replace("ö", "o");
            value = value.Replace("ğ", "g");
            value = value.Replace("ü", "u");
            value = value.Replace("İ", "I");
            value = value.Replace("Ş", "S");
            value = value.Replace("Ç", "C");
            value = value.Replace("Ö", "O");
            value = value.Replace("Ğ", "G");
            value = value.Replace("Ü", "U");
            return value;
        }
        public static string GetControlName(this FormDefinationField formDefination)
        {
            if (formDefination == null)
                return string.Empty;
            if (formDefination.ControlType == "CheckBox" || formDefination.ControlType == "RadioBox")
            {
                return $"chk{formDefination.TagName}";
            }
            else if (formDefination.ControlType == "Hidden")
            {
                return $"txt{formDefination.TagName}";
            }
            else if (formDefination.ControlType == "Text" || formDefination.ControlType == "DateTime")
            {
                return $"txt{formDefination.TagName}";
            }
            else
            {
                return $"cstmG{formDefination.TagName}";
            }

        }

        public static string GetControlName(this CustomeFieldItem formDefination)
        {

            if (formDefination.ControlType == "CheckBox" || formDefination.ControlType == "RadioBox")
            {
                return $"chk{formDefination.TagName}_{formDefination.CustomeFieldId}_{formDefination.Id}";
            }
            else if (formDefination.ControlType == "Hidden")
            {
                return $"txt{formDefination.TagName}_{formDefination.CustomeFieldId}_{formDefination.Id}";
            }
            else if (formDefination.ControlType == "Text" || formDefination.ControlType == "DateTime")
            {
                return $"txt{formDefination.TagName}_{formDefination.CustomeFieldId}_{formDefination.Id}";
            }

            return string.Empty;

        }

    }
}
