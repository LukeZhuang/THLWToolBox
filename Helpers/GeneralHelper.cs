using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Web;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string DATA_UPDATE_DATE = "2023/03/28";
        public static string StringFromDatabaseForDisplay(string originalText)
        {
            string text = originalText;
            text = text.Replace("<ret>", "ttt");
            Regex reg1 = new Regex("<color=(#[A-F0-9]+)>");
            text = reg1.Replace(text, "xxx$1yyy");
            text = Regex.Replace(text, "</color>", "zzz");
            text = HttpUtility.HtmlEncode(text);
            Regex reg2 = new Regex("xxx(#[A-F0-9]+)yyy");
            text = reg2.Replace(text, "<font color=\"$1\">");
            text = text.Replace("zzz", "</font>");
            text = text.Replace("\\n", "<br/>");
            text = text.Replace("ttt", "<br/>");
            return text;
        }
    }
}
