using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class TranslateDictionary
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public string Translate { get; set; }
        public string SearchText { get; set; }
    }
}
