using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace DemoNew.Models
{
    public class Translator
    {
       
        public static void Main(string[] args)
        {
            TranslateText(args[1], args[2]);
        }
        public static string TranslateText(string input, string languagePair)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);

            string result = String.Empty;

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = System.Text.Encoding.UTF7;
                result = webClient.DownloadString(url);
            }

            Match m = Regex.Match(result, "(?<=<div id=result_box dir=\"ltr\">)(.*?)(?=</div>)");
            if (m.Success) result = m.Value;

            return result;
        }


    }
}