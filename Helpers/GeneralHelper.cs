﻿using Microsoft.AspNetCore.Mvc;
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
        public static string VERSION_STR = "v1.2 alpha";
        public static string DATA_UPDATE_DATE = "2023/06/12";

        public static double CalcBulletPower(BulletMagazineModel bulletMagazine, PlayerUnitBulletData bulletRecord, PlayerUnitData pud, int level5PowerRate, double shotTypeWeight, bool IsCriticalRace)
        {
            double ATK = ((bulletRecord.type == 1) ? pud.yang_attack : pud.yin_attack) / 1000.0;
            double TotalPower = (bulletMagazine.bullet_power_rate / 100.0) * bulletMagazine.bullet_value;
            double Hit = (bulletRecord.hit / 100.0);
            double Critic = (1 + (IsCriticalRace ? 100.0 : bulletRecord.critical) / 100.0);
            double RangeWeight = (bulletMagazine.bullet_range == 2 ? 1.5 : 1.0);
            double PowerUpRate = level5PowerRate / 100.0;

            List<Tuple<int, int>> AddOns = new() {
                new Tuple<int, int> (bulletRecord.bullet1_addon_id, bulletRecord.bullet1_addon_value),
                new Tuple<int, int> (bulletRecord.bullet2_addon_id, bulletRecord.bullet2_addon_value),
                new Tuple<int, int> (bulletRecord.bullet3_addon_id, bulletRecord.bullet3_addon_value),
            };
            foreach (var AddOn in AddOns)
            {
                if (AddOn.Item1 == 1)
                    Hit = 1.0;
                /* hard */
                if (AddOn.Item1 == 4)
                    ATK += ((bulletRecord.type == 1) ? pud.yang_defense : pud.yin_defense) * (AddOn.Item2 / 100.0) / 1000.0;
                /* slash */
                if (AddOn.Item1 == 5)
                    ATK += pud.speed * (AddOn.Item2 / 100.0) / 1000.0;
            }

            return ATK * TotalPower * Hit * Critic * PowerUpRate * RangeWeight * shotTypeWeight;
        }
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
                rowText += string.Join("<br/>", shot.Item2);
                rowText += "</div>";

                rowText += "</div>";
                text += rowText;
            }
            return text;
        }

        public static string DisplayHitCheckOrder(List<Tuple<int, int, string, string>> hit_check_order_info)
        {
            string text = "";
            foreach (var orderInfo in hit_check_order_info)
            {
                text += "<div class=bullet-wrapper><b><font color=#FF6600>" + orderInfo.Item1 + "</font></b></div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item2 + "</div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item3 + "</div>";
                text += "<div class=bullet-wrapper>" + orderInfo.Item4 + "</div>";
            }
            Console.WriteLine(text);
            return text;
        }
    }
}
