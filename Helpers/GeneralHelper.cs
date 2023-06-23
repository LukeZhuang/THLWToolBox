using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models;
using static System.Net.Mime.MediaTypeNames;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v1.1 beta";
        public static string DATA_UPDATE_DATE = "2023/06/12";
        public static string StringFromDatabaseForDisplay(string originalText)
        {
            string text = originalText;
            text = text.Replace("<ret>", "rrr");
            Regex reg1 = new Regex("<color=(#[A-F0-9]+)>");
            text = reg1.Replace(text, "xxx$1yyy");
            text = Regex.Replace(text, "</color>", "zzz");
            text = text.Replace("<b>", "bb1bb");
            text = text.Replace("</b>", "bb2bb");
            text = HttpUtility.HtmlEncode(text);
            Regex reg2 = new Regex("xxx(#[A-F0-9]+)yyy");
            text = reg2.Replace(text, "<font color=\"$1\">");
            text = text.Replace("zzz", "</font>");
            text = text.Replace("\\n", "<br/>");
            text = text.Replace("rrr", "<br/>");
            text = text.Replace("bb1bb", "<b>");
            text = text.Replace("bb2bb", "</b>");
            return text;
        }

        public static string DisplayUnitLists(List<PlayerUnitData> playerUnitDatas, IUrlHelper urlHelper)
        {
            List<string> strs = new List<string>();
            int count = 0;
            foreach (var pud in playerUnitDatas)
            {
                string img = urlHelper.Content("~/res/units_img/" + pud.id + ".png");
                string curText = pud.name + pud.symbol_name + "xxx" + img + "yyy" + "ttt";
                count++;
                if (count % 5 == 0)
                {
                    curText += "rrr";
                }
                strs.Add(curText);
            }
            string text = String.Join("", strs);
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("xxx", "<img src=\"");
            text = text.Replace("yyy", "\".png\")\" width=\"40\" height=\"40\" />");
            text = text.Replace("rrr", "<br/>");
            text = text.Replace("ttt", "&emsp;");
            return text;
        }
    }
}
