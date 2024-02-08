using System.Text.RegularExpressions;
using System.Web;
using THLWToolBox.Models;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Models.EffectModel;

namespace THLWToolBox.Helpers
{
    public class GeneralHelper
    {
        public static string VERSION_STR = "v2.2";
        public static string DATA_UPDATE_DATE = "2024/02/06";

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
        public static PlayerUnitData? GetUnitByNameSymbol(List<PlayerUnitData> unitList, string unitSymbolName)
        {
            return unitList.Where(x => (x.name + x.symbol_name).Equals(unitSymbolName)).FirstOrDefault();
        }

        public static int? GetRaceIdByName(List<RaceData> raceList, string raceName)
        {
            return raceList.Where(x => x.name.Equals(raceName)).Select(x => x.id).FirstOrDefault();
        }

        public static PictureData? GetPictureByName(List<PictureData> pictureList, string pictureName)
        {
            return pictureList.Where(x => x.name.Equals(pictureName)).FirstOrDefault();
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
                                             int powerRate, double shotTypeWeight, bool isMatchedMagazine, bool isCriticalRace)
        {
            double atk = ((bulletRecord.type == 1) ? unitRecord.yang_attack : unitRecord.yin_attack) / 1000.0;
            double totalPower = (bulletMagazine.BulletPowerRate / 100.0) * bulletMagazine.BulletValue;
            double hit = (bulletRecord.hit / 100.0);
            double critic = (1 + (isCriticalRace ? 100.0 : bulletRecord.critical) / 100.0);
            double rangeWeight = (bulletMagazine.BulletRange == 2 ? AttackScoreWeights.RangeAll : AttackScoreWeights.RangeSolo);
            double powerUpRate = powerRate / 100.0;
            double matchedMagazineWeight = isMatchedMagazine ? 1.5 : 1.0;

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

            return atk * totalPower * hit * critic * powerUpRate * matchedMagazineWeight * rangeWeight * shotTypeWeight;
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

        public static List<SkillEffectInfo> GetUnitAbilitySkillEffectInfo(PlayerUnitData unitRecord, Dictionary<int, PlayerUnitAbilityData> abilityDict,
                                                                          bool useBoost, bool purgeBarrier)
        {
            List<EffectModel> abilityEffects = GetEffectModels(abilityDict[unitRecord.ability_id]);
            List<SkillEffectInfo> effectInfos = new();
            if (useBoost)
                effectInfos.Add(new SkillEffectInfo(1, "使用灵力的效果", new() { abilityEffects[0] }));
            if (purgeBarrier)
                effectInfos.Add(new SkillEffectInfo(1, "使用擦弹的效果", new() { abilityEffects[1] }));
            return effectInfos;
        }

        public static List<SkillEffectInfo> GetUnitSkillSkillEffectInfo(PlayerUnitData unitRecord, Dictionary<int, PlayerUnitSkillData> skillDict,
                                                                        Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict)
        {
            return new()
            {
                new SkillEffectInfo(2, "技能一效果", GetEffectModels(skillDict[unitRecord.skill1_id], skillEffectDict)),
                new SkillEffectInfo(2, "技能二效果", GetEffectModels(skillDict[unitRecord.skill2_id], skillEffectDict)),
                new SkillEffectInfo(2, "技能三效果", GetEffectModels(skillDict[unitRecord.skill3_id], skillEffectDict)),
            };
        }

        public static List<SkillEffectInfo> GetUnitSpellcardSkillEffectInfo(PlayerUnitData unitRecord, Dictionary<int, PlayerUnitSpellcardData> spellcardDict,
                                                                            Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict)
        {
            return new()
            {
                new SkillEffectInfo(3, "一符效果", GetEffectModels(spellcardDict[unitRecord.spellcard1_id], skillEffectDict)),
                new SkillEffectInfo(3, "二符效果", GetEffectModels(spellcardDict[unitRecord.spellcard2_id], skillEffectDict)),
                new SkillEffectInfo(3, "终符效果", GetEffectModels(spellcardDict[unitRecord.spellcard5_id], skillEffectDict)),
            };
        }

        public static List<SkillEffectInfo> GetUnitCharacteristicSkillEffectInfo(PlayerUnitData unitRecord, Dictionary<int, PlayerUnitCharacteristicData> characteristicDict)
        {
            List<EffectModel> characteristicEffects = GetEffectModels(characteristicDict[unitRecord.characteristic_id]);
            return new()
            {
                new SkillEffectInfo(4, "特性一效果", new() { characteristicEffects[0] }),
                new SkillEffectInfo(4, "特性二效果", new() { characteristicEffects[1] }),
                new SkillEffectInfo(4, "特性三效果", new() { characteristicEffects[2] }),
            };
        }

        static List<SkillEffectInfo> GetAttackSkillEffectInfo(AttackWithWeightModel attack, Dictionary<int, PlayerUnitBulletData> bulletDict,
                                                              Dictionary<int, BulletExtraEffectData> bulletExtraEffectDict)
        {
            return attack.AttackData.Magazines.Where(magazine => magazine.BulletId != 0)
                                              .Select(magazine => new SkillEffectInfo(5, attack.AttackData.AttackTypeName + "第" + magazine.MagazineId + "段",
                                                                                      GetEffectModels(bulletDict[magazine.BulletId], bulletExtraEffectDict, magazine.BulletRange)))
                                              .ToList();
        }

        public static List<SkillEffectInfo> GetUnitAttacksSkillEffectInfo(List<AttackWithWeightModel> attacks, Dictionary<int, PlayerUnitBulletData> bulletDict,
                                                                          Dictionary<int, BulletExtraEffectData> bulletExtraEffectDict)
        {
            return attacks.Select(attack => GetAttackSkillEffectInfo(attack, bulletDict, bulletExtraEffectDict)).SelectMany(x => x).ToList();
        }


        // select this unit if all effects from SelectBox are found in this unit's unitSkillEffectInfos
        public static bool EffectModelListMatchesSelectBox(List<SkillEffectInfo> unitAllSkillEffectInfos, List<EffectSelectBox> effectSelectBoxes)
        {
            if (effectSelectBoxes.Select(effectSelectBox => effectSelectBox.IsEffectiveSelectBox()).All(x => x == false))
                return false;
            List<EffectModel> unitAllEffectModels = unitAllSkillEffectInfos.SelectMany(skillEffectInfo => skillEffectInfo.Effects).ToList();
            return effectSelectBoxes.Select(effectSelectBox => effectSelectBox.EffectListMatchesSelectBox(unitAllEffectModels)).All(x => x);
        }

        // but only display this SkillEffectInfo if any of effect in it matches SelectBoxes
        static bool EffectModelListMatchesSelectBox(SkillEffectInfo skillEffectInfo, List<EffectSelectBox> effectSelectBoxes)
        {
            // TODO: highlight matched effects
            return effectSelectBoxes.Where(effectSelectBox => effectSelectBox.IsEffectiveSelectBox())
                                    .Select(effectSelectBox => effectSelectBox.EffectListMatchesSelectBox(skillEffectInfo.Effects)).Any(x => x);
        }

        public static List<SkillEffectInfo> GetMatchedSkillEffects(List<SkillEffectInfo> unitAllSkillEffectInfos, List<EffectSelectBox> effectSelectBoxes)
        {
            return unitAllSkillEffectInfos.Where(skillEffectInfo => EffectModelListMatchesSelectBox(skillEffectInfo, effectSelectBoxes)).ToList();
        }

        public static string GetSkillEffectInfoString(int skillEffectType)
        {
            return skillEffectType switch
            {
                1 => "能力",
                2 => "技能",
                3 => "符卡",
                4 => "特性",
                5 => "子弹",
                _ => throw new InvalidDataException(),
            };
        }

        public static string DisplayEffectString(EffectModel effectModel, bool? SimplifiedEffect, Dictionary<int, string>? RaceDict)
        {
            if (SimplifiedEffect.GetValueOrDefault(true))
                return new SimplifyEffectsHelper(effectModel, RaceDict).CreateSimplifiedEffectStr();
            return effectModel.OfficialDescription;
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
