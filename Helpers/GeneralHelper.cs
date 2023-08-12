using Azure.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v1.2 alpha";
        public static string DATA_UPDATE_DATE = "2023/06/12";

        // For HTML
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

        public static string GetWikiURL(string itemName)
        {
            string wikiURL = "https://wiki.biligame.com/touhoulostword/" + itemName;
            return "<a href=" + wikiURL + ">" + itemName + "</a>";
        }


        // Common functions which are used in mutliple controllers
        public static PlayerUnitData GetUnitByNameSymbol(List<PlayerUnitData> unitList, string unitSymbolName)
        {
            return unitList.Where(x => (x.name + x.symbol_name).Equals(unitSymbolName)).First();
        }

        public static int GetRaceIdByName(List<RaceData> raceList, string raceName)
        {
            return raceList.Where(x => x.name.Equals(raceName)).Select(x => x.id).First();
        }

        public static List<BulletAddonModel> GetBulletAddons(PlayerUnitBulletData bulletRecord)
        {
            return new List<BulletAddonModel>() {
                new BulletAddonModel(bulletRecord.bullet1_addon_id, bulletRecord.bullet1_addon_value),
                new BulletAddonModel(bulletRecord.bullet2_addon_id, bulletRecord.bullet2_addon_value),
                new BulletAddonModel(bulletRecord.bullet3_addon_id, bulletRecord.bullet3_addon_value),
            };
        }

        public static double CalcBulletPower(BulletMagazineModel bulletMagazine, PlayerUnitBulletData bulletRecord, PlayerUnitData unitRecord,
                                             int powerRate, double shotTypeWeight, bool isCriticalRace)
        {
            double atk = ((bulletRecord.type == 1) ? unitRecord.yang_attack : unitRecord.yin_attack) / 1000.0;
            double totalPower = (bulletMagazine.BulletPowerRate / 100.0) * bulletMagazine.BulletValue;
            double hit = (bulletRecord.hit / 100.0);
            double critic = (1 + (isCriticalRace ? 100.0 : bulletRecord.critical) / 100.0);
            double rangeWeight = (bulletMagazine.BulletRange == 2 ? AttackScoreWeights.RangeAll : AttackScoreWeights.RangeSolo);
            double powerUpRate = powerRate / 100.0;

            List<BulletAddonModel> bulletAddons = GetBulletAddons(bulletRecord);
            foreach (var bulletAddon in bulletAddons)
            {
                if (bulletAddon.Id == (int)BulletAddonType.AbsHit)
                    hit = 1.0;
                else if (bulletAddon.Id == (int)BulletAddonType.Hard)
                    atk += ((bulletRecord.type == 1) ? unitRecord.yang_defense : unitRecord.yin_defense) * (bulletAddon.Value / 100.0) / 1000.0;
                else if (bulletAddon.Id == (int)BulletAddonType.Slash)
                    atk += unitRecord.speed * (bulletAddon.Value / 100.0) / 1000.0;
            }

            return atk * totalPower * hit * critic * powerUpRate * rangeWeight * shotTypeWeight;
        }

        public static List<AttackWithWeightModel> GetUnitAttacksWithWeight(PlayerUnitData unitRecord, AttackSelectionModel attackSelection,
                                                                           Dictionary<int, PlayerUnitShotData> shotDict, Dictionary<int, PlayerUnitSpellcardData> spellcardDict)
        {
            List<AttackWithWeightModel> attacks = new();
            if (attackSelection.SpreadShot)
                attacks.Add(new AttackWithWeightModel(new AttackData(AttackData.TypeStringSpreadShot, shotDict[unitRecord.shot1_id]), AttackScoreWeights.TypeSpreadShot));
            if (attackSelection.FocusShot)
                attacks.Add(new AttackWithWeightModel(new AttackData(AttackData.TypeStringFocusShot, shotDict[unitRecord.shot2_id]), AttackScoreWeights.TypeFocusShot));
            if (attackSelection.NormalSpellcard)
            {
                attacks.Add(new AttackWithWeightModel(new AttackData(AttackData.TypeStringSpellcard1, spellcardDict[unitRecord.spellcard1_id]), AttackScoreWeights.TypeNormalSpellcard));
                attacks.Add(new AttackWithWeightModel(new AttackData(AttackData.TypeStringSpellcard2, spellcardDict[unitRecord.spellcard2_id]), AttackScoreWeights.TypeNormalSpellcard));
            }
            if (attackSelection.LastWord)
                attacks.Add(new AttackWithWeightModel(new AttackData(AttackData.TypeStringLastWord, spellcardDict[unitRecord.spellcard5_id]), AttackScoreWeights.TypeLastWord));
            return attacks;
        }

        public static string CreateRacesStringOfUnit(HashSet<int> raceIdSet, List<RaceData> raceList, int? highlightRaceId)
        {
            List<string> races = new();

            foreach (var raceRecord in raceList)
            {
                if (raceIdSet.Contains(raceRecord.id))
                {
                    string raceName = raceRecord.name;
                    if (highlightRaceId != null && raceRecord.id == highlightRaceId)
                        raceName = "<b><color=#FF6600>" + raceName + "</color></b>";
                    races.Add(raceName);
                }
            }

            return string.Join(", ", races);
        }

        public static List<UnitRaceDisplayModel> CreateUnitRaceDisplayModelByUnit(PlayerUnitData unitRecord, HashSet<int> raceIds,
                                                                                  List<RaceData> raceList, int? highlightRaceId)
        {
            if (raceIds.Count == 0)
                return new();
            string racesString = CreateRacesStringOfUnit(raceIds, raceList, highlightRaceId);
            return new() { new UnitRaceDisplayModel(unitRecord, racesString) };
        }


        // Other helper functions
        public static List<T?> RemoveNullElements<T>(List<T?> list)
        {
            return list.Where(x => x != null).ToList();
        }

        public static List<T> CastToNonNullList<T>(List<T?> list)
        {
            // make sure RemoveNullElements is called before this function is used
            if (list.Where(x => x == null).Any())
                throw new NullReferenceException("Must call \"RemoveNullElements\" before calling \"CastToNonNullList\"");
            return list.Cast<T>().ToList();
        }
    }
}
