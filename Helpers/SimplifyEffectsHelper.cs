using THLWToolBox.Models;
using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Helpers
{
    public class SimplifyEffectsHelper
    {
        private const string VALUE_COLOR = "#FC0377";

        public EffectModel EffectModel { get; set; }
        Dictionary<int, string>? RaceDict {  get; set; }

        public SimplifyEffectsHelper(EffectModel effectModel, Dictionary<int, string>? raceDict)
        {
            EffectModel = effectModel;
            RaceDict = raceDict;
        }

        string CreateRangeString()
        {
            return EffectModel.Range switch
            {
                0 or 1 => "自身",
                2 => "己全",
                3 => "敌单",
                4 => "敌全",
                _ => throw new NotImplementedException(),
            };
        }

        static bool IsUnitRoleSpecificBuff(int type)
        {
            return type >= 21 && type <= 28;
        }

        static bool IsUnitRoleSpecificDebuff(int type)
        {
            return type >= 31 && type <= 38;
        }

        static int IsBuffOrDebuff(int type)
        {
            if (type == 1 || IsUnitRoleSpecificBuff(type))
                return 1;
            else if (type == 2 || IsUnitRoleSpecificDebuff(type))
                return -1;
            return 0;
        }

        string CreateRolePrefixString()
        {
            int role, effectType = EffectModel.EffectType;
            if (IsUnitRoleSpecificBuff(effectType))
                role = effectType - 20;
            else if (IsUnitRoleSpecificDebuff(effectType))
                role = effectType - 30;
            else
                return "";
            return GetUnitRoleString(role) + "用时";
        }

        private static string MarkValue(int value)
        {
            return "<b><color=" + VALUE_COLOR + ">" + Convert.ToString(value) + "</color></b>";
        }
        private static string MarkValue(double value)
        {
            return "<b><color=" + VALUE_COLOR + ">" + string.Format("{0:N2}", value) + "</color></b>";
        }

        public string CreateSimplifiedEffectStr()
        {
            if (IsBuffOrDebuff(EffectModel.EffectType) != 0)
                return CreateRolePrefixString() + CreateRangeString() + GetBuffDebuffTypeString(EffectModel.SubEffectType) +
                       (IsBuffOrDebuff(EffectModel.EffectType) == 1 ? "加" : "减") + MarkValue(EffectModel.Value) + "级(" + Convert.ToString(EffectModel.Turn) + "t)";

            return EffectModel.EffectType switch
            {
                0 => "",
                3 => CreateRangeString() + "回" + MarkValue(EffectModel.Value) + "%血",
                4 => CreateRangeString() + "回" + MarkValue(EffectModel.Value) + "盾",
                5 => CreateRangeString() + "加" + MarkValue(0.05 * EffectModel.Value) + "P",
                12 => CreateRangeString() + "受到" + GetBulletTypeString(EffectModel.SubEffectType) + "弹减伤" + MarkValue(EffectModel.Value) + "%",
                13 => CreateRangeString() + "受到" + GetElementTypeString(EffectModel.SubEffectType) + "属性减伤" + MarkValue(EffectModel.Value) + "%",
                14 => CreateRangeString() + "受到" + (RaceDict == null ? "某种族" : "\"" + RaceDict[EffectModel.SubEffectType] + "\"") + "减伤" + MarkValue(EffectModel.Value) + "%",
                15 => GetBulletTypeString(EffectModel.SubEffectType) + "弹加伤" + MarkValue(EffectModel.Value) + "%",
                16 => GetElementTypeString(EffectModel.SubEffectType) + "属性加伤" + MarkValue(EffectModel.Value) + "%",
                17 => "灵力回收效率加" + MarkValue(EffectModel.Value) + "%",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
