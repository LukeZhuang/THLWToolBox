using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Helpers
{
    public class SimplifyEffectsHelper
    {
        private const string VALUE_COLOR = "#FC0377";
        private static string RangeStr(int range)
        {
            return range switch
            {
                0 or 1 => "自身",
                2 => "己全",
                3 => "敌单",
                4 => "敌全",
                _ => throw new NotImplementedException(),
            };
        }

        private static bool IsUnitRoleSpecificBuff(int type)
        {
            return type >= 21 && type <= 28;
        }

        private static bool IsUnitRoleSpecificDebuff(int type)
        {
            return type >= 31 && type <= 38;
        }

        private static int BuffDebuff(int type)
        {
            if (type == 1 || IsUnitRoleSpecificBuff(type))
                return 1;
            else if (type == 2 || IsUnitRoleSpecificDebuff(type))
                return -1;
            return 0;
        }

        private static string RolePrefix(int type)
        {
            int role;
            if (IsUnitRoleSpecificBuff(type))
                role = type - 20;
            else if (IsUnitRoleSpecificDebuff(type))
                role = type - 30;
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

        public static string CreateSimplifiedEffectStr(int type, int subtype, int value, int turn, int range, IDictionary<int, string>? raceDict = null)
        {
            if (BuffDebuff(type) != 0)
                return RolePrefix(type) + RangeStr(range) + GetBuffDebuffTypeString(subtype) + (BuffDebuff(type) == 1 ? "加" : "减") + MarkValue(value) + "级(" + Convert.ToString(turn) + "t)";

            return type switch
            {
                0 => "",
                3 => RangeStr(range) + "回" + MarkValue(value) + "%血",
                4 => RangeStr(range) + "回" + MarkValue(value) + "盾",
                5 => RangeStr(range) + "加" + MarkValue(0.05 * value) + "P",
                12 => RangeStr(range) + "受到" + GetBulletTypeString(subtype) + "弹减伤" + MarkValue(value) + "%",
                13 => RangeStr(range) + "受到" + GetElementTypeString(subtype) + "属性减伤" + MarkValue(value) + "%",
                14 => RangeStr(range) + "受到" + (raceDict == null ? "某种族" : "\"" + raceDict[subtype] + "\"") + "减伤" + MarkValue(value) + "%",
                15 => GetBulletTypeString(subtype) + "弹加伤" + MarkValue(value) + "%",
                16 => GetElementTypeString(subtype) + "属性加伤" + MarkValue(value) + "%",
                17 => "灵力回收效率加" + MarkValue(value) + "%",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
