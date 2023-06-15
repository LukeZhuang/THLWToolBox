namespace THLWToolBox.Helpers
{
    public class SimplifyEffectsHelper
    {
        private static string RangeStr(int range)
        {
            switch (range)
            {
                case 0:
                case 1:
                    return "自身";
                case 2:
                    return "己全";
                case 3:
                    return "敌单";
                case 4:
                    return "敌全";
                default:
                    throw new NotImplementedException();
            }
        }
        private static int BuffDebuff(int type)
        {
            if (type == 1 || (type >= 21 && type <= 28))
                return 1;
            else if (type == 2 || (type >= 31 && type <= 38))
                return -1;
            return 0;
        }

        private static string BuffDebuffSubTypeStr(int subtype)
        {
            switch (subtype)
            {
                case 1:
                    return "阳攻";
                case 2:
                    return "阳防";
                case 3:
                    return "阴攻";
                case 4:
                    return "阴防";
                case 5:
                    return "速度";
                case 6:
                    return "命中";
                case 7:
                    return "回避";
                case 8:
                    return "会心攻击";
                case 9:
                    return "会心防御";
                case 10:
                    return "会心命中";
                case 11:
                    return "会心回避";
                case 12:
                    return "仇恨值";
                default:
                    throw new NotImplementedException();
            }
        }

        private static string RolePrefix(int type)
        {
            int role = 0;
            if (type >= 21 && type <= 28)
                role = type - 20;
            else if (type >= 31 && type <= 38)
                role = type - 30;
            else
                return "";
            switch (role)
            {
                case 1:
                    return "防御式用时";
                case 2:
                    return "支援式用时";
                case 3:
                    return "回复式用时";
                case 4:
                    return "干扰式用时";
                case 5:
                    return "攻击式用时";
                case 6:
                    return "技巧式用时";
                case 7:
                    return "速攻式用时";
                case 8:
                    return "破坏式用时";
                default:
                    throw new NotImplementedException();
            }
        }

        private static string BulletTypeStr(int subtype)
        {
            switch (subtype)
            {
                case 1:
                    return "通常弹";
                case 2:
                    return "镭射弹";
                case 3:
                    return "体术弹";
                case 4:
                    return "斩击弹";
                case 5:
                    return "动能弹";
                case 6:
                    return "流体弹";
                case 7:
                    return "能量弹";
                case 8:
                    return "御符弹";
                case 9:
                    return "光弹";
                case 10:
                    return "尖头弹";
                case 11:
                    return "追踪弹";
                default:
                    throw new NotImplementedException();
            }
        }

        private static string ElementTypeStr(int subtype)
        {
            switch (subtype)
            {
                case 1:
                    return "日属性";
                case 2:
                    return "月属性";
                case 3:
                    return "火属性";
                case 4:
                    return "水属性";
                case 5:
                    return "木属性";
                case 6:
                    return "金属性";
                case 7:
                    return "土属性";
                case 8:
                    return "星属性";
                case 9:
                    return "无属性";
                default:
                    throw new NotImplementedException();
            }
        }

        private static string MarkValue(int value)
        {
            return "<color=#FF6600>" + Convert.ToString(value) + "</color>";
        }
        private static string MarkValue(double value)
        {
            return "<color=#FF6600>" + string.Format("{0:N2}", value) + "</color>";
        }

        public static string CreateSimplifiedEffectStr(int type, int subtype, int value, int turn, int range, IDictionary<int, string>? raceDict = null)
        {
            if (type == 0)
            {
                return "";
            }
            else if (BuffDebuff(type) != 0)
            {
                return RolePrefix(type) + RangeStr(range) + BuffDebuffSubTypeStr(subtype) + (BuffDebuff(type) == 1 ? "加" : "减") + MarkValue(value) + "级(" + Convert.ToString(turn) + "t)";
            }
            else if (type == 3)
            {
                return RangeStr(range) + "回" + MarkValue(value) + "%血";
            }
            else if (type == 4)
            {
                return RangeStr(range) + "回" + MarkValue(value) + "盾";
            }
            else if (type == 5)
            {
                return RangeStr(range) + "加" + MarkValue(0.05 * value) + "P";
            }
            else if (type == 12)
            {
                return RangeStr(range) + "受到" + BulletTypeStr(subtype) + "减伤" + MarkValue(value) + "%";
            }
            else if (type == 13)
            {
                return RangeStr(range) + "受到" + ElementTypeStr(subtype) + "减伤" + MarkValue(value) + "%";
            }
            else if (type == 14)
            {
                return RangeStr(range) + "受到" + (raceDict == null ? "某种族" : "\"" + raceDict[subtype] + "\"") + "减伤" + MarkValue(value) + "%";
            }
            else if (type == 15)
            {
                return BulletTypeStr(subtype) + "加伤" + MarkValue(value) + "%";
            }
            else if (type == 16)
            {
                return ElementTypeStr(subtype) + "加伤" + MarkValue(value) + "%";
            }
            else if (type == 17)
            {
                return "灵力回收效率加" + MarkValue(value) + "%";
            }
            else
                throw new NotImplementedException();
        }
    }
}
