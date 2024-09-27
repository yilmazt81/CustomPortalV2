using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateApp
{
    public class TranslateTextDTO
    {

        public string Text { get; set; }

        public string SourceLangeuage { get; set; }
        public string TargetLanguage { get; set; }

    }


    public class TranslateTextReturn
    {
        public Translation[] translations { get; set; }
    }



    public class Translation
    {
        public string text { get; set; }
        public string to { get; set; }
    }

}
