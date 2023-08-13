using Microsoft.AspNetCore.Mvc.Rendering;
using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Models.GeneralModels;
using static THLWToolBox.Helpers.GeneralHelper;
using THLWToolBox.Helpers;

namespace THLWToolBox.Models.ViewModels
{
    public class SkillEffectInfo
    {
        public int Type { get; set; }
        public string SubType { get; set; }
        public List<EffectModel> Effects { get; set; }
        public SkillEffectInfo(int type, string subType, List<EffectModel> effects)
        {
            Type = type;
            SubType = subType;
            Effects = effects;
        }
    }
    public class UnitSkillDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public List<SkillEffectInfo> SkillEffectInfos { get; set; }
        public UnitSkillDisplayModel(PlayerUnitData unit, List<SkillEffectInfo> skillEffectInfos)
        {
            Unit = unit;
            SkillEffectInfos = skillEffectInfos;
        }
    }
    public class UnitSkillFilterViewModel
    {
        // Display models
        public List<UnitSkillDisplayModel>? UnitSkillInfos {  get; set; }

        // Select Lists
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public SelectList? TurnTypes { get; set; }

        // Webpage query Parameters
        public bool? AbilityBoost { get; set; }
        public bool? AbilityPurgeBarrier { get; set; }
        public bool? Skill { get; set; }
        public bool? Spellcard { get; set; }
        public bool? Characteristic { get; set; }
        public int? EffectId1 { get; set; }
        public int? SubeffectId1 { get; set; }
        public int? Range1 { get; set; }
        public int? TurnTypeId1 { get; set; }
        public int? EffectId2 { get; set; }
        public int? SubeffectId2 { get; set; }
        public int? Range2 { get; set; }
        public int? TurnTypeId2 { get; set; }
        public int? EffectId3 { get; set; }
        public int? SubeffectId3 { get; set; }
        public int? Range3 { get; set; }
        public int? TurnTypeId3 { get; set; }
        public bool? SimplifiedEffect { get; set; }

        // Other Helper Data
        public Dictionary<int, string>? RaceDict { get; set; }


        // Methods
        public List<EffectSelectBox> CreateEffectSelectBoxes()
        {
            return new()
            {
                new EffectSelectBox(1, EffectId1, SubeffectId1, Range1, null, TurnTypeId1),
                new EffectSelectBox(2, EffectId2, SubeffectId2, Range2, null, TurnTypeId2),
                new EffectSelectBox(3, EffectId3, SubeffectId3, Range3, null, TurnTypeId3),
            };
        }

        static string GetSkillEffectInfoString(int skillEffectType)
        {
            return skillEffectType switch
            {
                1 => "能力",
                2 => "技能",
                3 => "符卡",
                4 => "特性",
                _ => throw new InvalidDataException(),
            };
        }

        string DisplayEffectString(EffectModel effectModel)
        {
            if (SimplifiedEffect.GetValueOrDefault(true))
                return new SimplifyEffectsHelper(effectModel, RaceDict).CreateSimplifiedEffectStr();
            return effectModel.OfficialDescription;
        }

        public string DisplayUnitSkills(List<SkillEffectInfo> SkillEffectInfos)
        {
            string skillGridWrapper = "<div class=\"skill-wrapper\">";
            string text = "<div class=\"skills-grid\">";
            text += skillGridWrapper + "类型" + "</div>";
            text += skillGridWrapper + "描述" + "</div>";
            text += skillGridWrapper + "效果" + "</div>";
            foreach (var skillEffectInfo in SkillEffectInfos)
            {
                text += skillGridWrapper + GetSkillEffectInfoString(skillEffectInfo.Type) + "</div>";
                text += skillGridWrapper + skillEffectInfo.SubType + "</div>";
                text += skillGridWrapper + string.Join("<br>", skillEffectInfo.Effects.Select(x => StringFromDatabaseForDisplay(DisplayEffectString(x)))) + "</div>";
            }
            text += "</div>";
            return text;
        }
    }
}
