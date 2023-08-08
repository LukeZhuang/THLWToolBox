using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v1.2 alpha";
        public static string DATA_UPDATE_DATE = "2023/06/12";

        public static double CalcBulletPower(BulletMagazineModel bulletMagazine, PlayerUnitBulletData bulletRecord, PlayerUnitData unitRecord,
                                             int powerRate, double shotTypeWeight, bool isCriticalRace)
        {
            double atk = ((bulletRecord.type == 1) ? unitRecord.yang_attack : unitRecord.yin_attack) / 1000.0;
            double totalPower = (bulletMagazine.bullet_power_rate / 100.0) * bulletMagazine.bullet_value;
            double hit = (bulletRecord.hit / 100.0);
            double critic = (1 + (isCriticalRace ? 100.0 : bulletRecord.critical) / 100.0);
            double rangeWeight = (bulletMagazine.bullet_range == 2 ? 1.5 : 1.0);
            double powerUpRate = powerRate / 100.0;

            List<BulletAddonModel> AddOns = new() {
                new BulletAddonModel((BulletAddonType)bulletRecord.bullet1_addon_id, bulletRecord.bullet1_addon_value),
                new BulletAddonModel((BulletAddonType)bulletRecord.bullet2_addon_id, bulletRecord.bullet2_addon_value),
                new BulletAddonModel((BulletAddonType)bulletRecord.bullet3_addon_id, bulletRecord.bullet3_addon_value),
            };
            foreach (var AddOn in AddOns)
            {
                if (AddOn.id == BulletAddonType.AbsHit)
                    hit = 1.0;
                else if (AddOn.id == BulletAddonType.Hard)
                    atk += ((bulletRecord.type == 1) ? unitRecord.yang_defense : unitRecord.yin_defense) * (AddOn.value / 100.0) / 1000.0;
                else if (AddOn.id == BulletAddonType.Slash)
                    atk += unitRecord.speed * (AddOn.value / 100.0) / 1000.0;
            }

            return atk * totalPower * hit * critic * powerUpRate * rangeWeight * shotTypeWeight;
        }

        public static string GetImageHtmlRaw(string imgUrl)
        {
            return "<div class=\"picture-unit-img-wrapper\"><img src=\"" + imgUrl + "\" alt=\"暂无图片\" onerror=\"this.src='/res/website/noimg.png';\" /></div>";
        }

        public static string StringFromDatabaseForDisplay(string originalText)
        {
            string text = originalText;
            text = text.Replace("<ret>", "@RETURNLINE@");
            Regex reg1 = new Regex("<color=(#[A-F0-9]+)>");
            text = reg1.Replace(text, "@COLORSTART$1COLORSTART@");
            text = Regex.Replace(text, "</color>", "@COLOREND@");
            text = text.Replace("<b>", "@BOLDSTART@");
            text = text.Replace("</b>", "@BOLDEND@");
            text = HttpUtility.HtmlEncode(text);
            Regex reg2 = new Regex("@COLORSTART(#[A-F0-9]+)COLORSTART@");
            text = reg2.Replace(text, "<font color=\"$1\">");
            text = text.Replace("@COLOREND@", "</font>");
            text = text.Replace("\\n", "<br/>");
            text = text.Replace("@RETURNLINE@", "<br/>");
            text = text.Replace("@BOLDSTART@", "<b>");
            text = text.Replace("@BOLDEND@", "</b>");
            return text;
        }
    }
}
