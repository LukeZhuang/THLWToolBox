using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Drawing.Printing;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models;
using static System.Net.Mime.MediaTypeNames;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v1.1";
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
            foreach (var pud in playerUnitDatas)
            {
                string img = urlHelper.Content("~/res/units_img/" + pud.id + ".png");
                string curText = "zzz" + pud.name + pud.symbol_name + "xxx" + img + "yyy";
                strs.Add(curText);
            }
            string text = String.Join("", strs);
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("zzz", "<div class=\"units-img-name-box\">");
            text = text.Replace("xxx", "<img src=\"");
            string alt_img = HttpUtility.HtmlEncode(urlHelper.Content("~/res/website/noimg.png"));
            text = text.Replace("yyy", "\" alt=\"暂无图片\" onerror=\"this.src='" + alt_img + "';\" /></div>");
            Console.WriteLine(text);
            return text;
        }
    }
}
