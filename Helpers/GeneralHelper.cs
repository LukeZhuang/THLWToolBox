using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models;
using static System.Net.Mime.MediaTypeNames;
using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v1.1 alpha";
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
            return text;
        }

        public static string BulletStrStyle(int BulletStatus, string ElementStr)
        {
            // 0-bit: yin/yang
            // 1-bit: selected by main
            // 2-bit: selected by sub

            string color = "";
            bool selected = true;

            if ((BulletStatus >> 1 & 1) == 1)
                color = "#FF6600";
            else if ((BulletStatus >> 2 & 1) == 1)
                color = "#4CAFFF";
            else
            {
                color = "#000000";
                selected = false;
            }

            string str = "<font color=" + color + ">" + ElementStr + "</font>";
            if (selected)
            {
                str = "<b>" + str + "</b>";
            }
            return str;
        }

        public static string DisplayShotElements(List<Tuple<string, List<Tuple<PlayerUnitBulletData?, int>>>> unitBulletList)
        {
            string text = "";
            foreach (var shot in unitBulletList)
            {
                string rowText = "<div class=shot-row>";
                string shotName = shot.Item1;
                rowText += "<div class=shot-name-grid>" + shotName + "</div>";
                rowText += "<div class=element-grids>";
                foreach (var bulletRecord in shot.Item2)
                {
                    PlayerUnitBulletData? bullet = bulletRecord.Item1;
                    int status = bulletRecord.Item2;
                    string bulletStr = (bullet == null ? "null" : BulletStrStyle(status, GetElementTypeString(bullet.element) + "-" + GetBulletTypeString(bullet.category)));
                    rowText += "<div class=\"element-grid " + ((status & 1) == 1 ? "yang-background" : "yin-background") + "\">" + bulletStr + "</div>";
                }
                rowText += "</div>";
                rowText += "</div>";
                text += rowText;
            }
            return text;
        }

        public static string DisplayShotCriticals(List<Tuple<string, List<string>>> unitBulletList)
        {
            string text = "";
            foreach (var shot in unitBulletList)
            {
                string rowText = "<div class=shot-row>";
                string shotName = shot.Item1;
                rowText += "<div class=shot-name-grid>" + shotName + "</div>";

                rowText += "<div class=critical-bullet-rows>";
                rowText += string.Join(", ", shot.Item2);
                rowText += "</div>";

                rowText += "</div>";
                text += rowText;
            }
            return text;
        }
    }
}
