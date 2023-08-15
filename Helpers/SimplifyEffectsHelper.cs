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

        int IsBuffOrDebuff()
        {
            if (EffectModel.EffectType == 1 || IsUnitRoleSpecificBuff(EffectModel.EffectType) || EffectModel.EffectType == 41)
                return 1;
            else if (EffectModel.EffectType == 2 || IsUnitRoleSpecificDebuff(EffectModel.EffectType) || EffectModel.EffectType == 42)
                return -1;
            return 0;
        }

        string Create2ndRankString()
        {
            if (EffectModel.EffectType == 41 || EffectModel.EffectType == 42)
                return "2阶";
            return "";
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

        static string MarkValue(int value)
        {
            return "<b><color=" + VALUE_COLOR + ">" + Convert.ToString(value) + "</color></b>";
        }
        static string MarkValue(double value)
        {
            return "<b><color=" + VALUE_COLOR + ">" + string.Format("{0:N2}", value) + "</color></b>";
        }

        string CreateAddValueSuffix()
        {
            if (EffectModel.SuccessRate != 0)
            {
                if (EffectModel.AddValue == 0)
                    return "[发生率" + EffectModel.SuccessRate + "%]";
                else
                {
                    if (EffectModel.SuccessRate == 100)
                    {
                        Console.WriteLine(EffectModel.Name + " " + EffectModel.EffectType + " " + EffectModel.SubEffectType + " " + EffectModel.SuccessRate);
                        throw new InvalidDataException();
                    }
                    return "[" + EffectModel.SuccessRate + "%概率+" + (EffectModel.EffectType == 5 ? 0.05 * EffectModel.AddValue : EffectModel.AddValue) + "]";
                }
            }
            return "";
        }

        public string CreateSimplifiedEffectStr()
        {
            string simplifiedEffectStr;
            if (EffectModel.EffectType == 0)
                return "";
            else if (IsBuffOrDebuff() != 0)
                simplifiedEffectStr = CreateRolePrefixString() + CreateRangeString() + Create2ndRankString() + GetBuffDebuffTypeString(EffectModel.SubEffectType) +
                                      (IsBuffOrDebuff() == 1 ? "加" : "减") + MarkValue(EffectModel.Value) + "级(" + Convert.ToString(EffectModel.Turn) + "t)";
            else
                simplifiedEffectStr = EffectModel.EffectType switch
                {
                    3 => CreateRangeString() + "回" + MarkValue(EffectModel.Value) + "%血",
                    4 => CreateRangeString() + "回" + MarkValue(EffectModel.Value) + "盾",
                    5 => CreateRangeString() + "加" + MarkValue(0.05 * EffectModel.Value) + "P",
                    6 => CreateRangeString() + "加" + MarkValue(EffectModel.Value) + "枚" + "异常[" + GetBarrierTypeString(EffectModel.SubEffectType) + "](" + Convert.ToString(EffectModel.Turn) + "t)",
                    7 => CreateRangeString() + GetEnemyOrderChangeTypeString(EffectModel.SubEffectType),
                    8 => GetActOrderChangeTypeString(EffectModel.SubEffectType),
                    9 => CreateRangeString() + "清" + MarkValue(EffectModel.Value) + "枚异常",
                    10 => CreateRangeString() + "解除禁止状态/强度下降",
                    11 => GetDamageReduceTypeString(EffectModel.SubEffectType) + MarkValue(EffectModel.Value) + "%",
                    12 => CreateRangeString() + "受到" + GetBulletTypeString(EffectModel.SubEffectType) + "弹减伤" + MarkValue(EffectModel.Value) + "%",
                    13 => CreateRangeString() + "受到" + GetElementTypeString(EffectModel.SubEffectType) + "属性减伤" + MarkValue(EffectModel.Value) + "%",
                    14 => CreateRangeString() + "受到" + (RaceDict == null ? "某种族" : "\"" + RaceDict[EffectModel.SubEffectType] + "\"") + "减伤" + MarkValue(EffectModel.Value) + "%",
                    15 => GetBulletTypeString(EffectModel.SubEffectType) + "弹加伤" + MarkValue(EffectModel.Value) + "%",
                    16 => GetElementTypeString(EffectModel.SubEffectType) + "属性加伤" + MarkValue(EffectModel.Value) + "%",
                    17 => "灵力回收效率加" + MarkValue(EffectModel.Value) + "%",
                    _ => throw new NotImplementedException(),
                };
            return EffectModel.TimingString + simplifiedEffectStr + CreateAddValueSuffix();
        }
    }
}
