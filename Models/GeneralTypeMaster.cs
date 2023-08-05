using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace THLWToolBox.Models
{
    public class GeneralTypeMaster
    {
        public class BulletMagazineModel
        {
            public int bullet_id { get; set; }
            public int bullet_range { get; set; }
            public int bullet_value { get; set; }
            public int bullet_power_rate { get; set; }
            public BulletMagazineModel(int bullet_id, int bullet_range, int bullet_value, int bullet_power_rate)
            {
                this.bullet_id = bullet_id;
                this.bullet_range = bullet_range;
                this.bullet_value = bullet_value;
                this.bullet_power_rate = bullet_power_rate;
            }
        }

        public class EffectModel
        {
            public int effect { get; set; }
            public int sub_effect { get; set; }
            public int range { get; set; }
            public int unit_role { get; set; }
            public int turn { get; set; }
            public EffectModel(int effect, int sub_effect, int range, int unit_role, int turn)
            {
                this.effect = effect;
                this.sub_effect = sub_effect;
                this.range = range;
                this.unit_role = unit_role;
                this.turn = turn;
            }
        }

        public static string GetRangeTypeString(int range)
        {
            switch (range)
            {
                case 0:
                    return "装备者";
                case 1:
                    return "自身";
                case 2:
                    return "己方全体";
                case 3:
                    return "敌方单体";
                case 4:
                    return "敌方全体";
                default:
                    throw new NotImplementedException();
            }
        }
        public static string GetBuffDebuffTypeString(int effectType, bool isBuff)
        {
            string prefix = isBuff ? "Buff-" : "Debuff-";
            string suffix = isBuff ? "上升" : "下降";
            string buffDebuffStr = "";
            switch (effectType)
            {
                case 1:
                    buffDebuffStr = "阳攻";
                    break;
                case 2:
                    buffDebuffStr = "阳防";
                    break;
                case 3:
                    buffDebuffStr = "阴攻";
                    break;
                case 4:
                    buffDebuffStr = "阴防";
                    break;
                case 5:
                    buffDebuffStr = "速度";
                    break;
                case 6:
                    buffDebuffStr = "命中";
                    break;
                case 7:
                    buffDebuffStr = "回避";
                    break;
                case 8:
                    buffDebuffStr = "会心攻击";
                    break;
                case 9:
                    buffDebuffStr = "会心防御";
                    break;
                case 10:
                    buffDebuffStr = "会心命中";
                    break;
                case 11:
                    buffDebuffStr = "会心回避";
                    break;
                case 12:
                    buffDebuffStr = "仇恨值";
                    break;
                default:
                    throw new NotImplementedException();
            }
            return prefix + buffDebuffStr + suffix;
        }

        public static string GetBulletTypeString(int bulletType)
        {
            switch (bulletType)
            {
                case 1:
                    return "通常";
                case 2:
                    return "镭射";
                case 3:
                    return "体术";
                case 4:
                    return "斩击";
                case 5:
                    return "动能";
                case 6:
                    return "流体";
                case 7:
                    return "能量";
                case 8:
                    return "御符";
                case 9:
                    return "光";
                case 10:
                    return "尖头";
                case 11:
                    return "追踪";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetBulletTypeStringForSelect(int elementType)
        {
            return "弹种-" + GetBulletTypeString(elementType) + "弹";
        }

        public static string GetElementTypeString(int elementType)
        {
            switch (elementType)
            {
                case 1:
                    return "日";
                case 2:
                    return "月";
                case 3:
                    return "火";
                case 4:
                    return "水";
                case 5:
                    return "木";
                case 6:
                    return "金";
                case 7:
                    return "土";
                case 8:
                    return "星";
                case 9:
                    return "无";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetElementTypeStringForSelect(int elementType)
        {
            return "属性-" + GetElementTypeString(elementType);
        }

        public static string GetUnitRoleString(int unitRole)
        {
            switch (unitRole)
            {
                case 1:
                    return "防御式";
                case 2:
                    return "支援式";
                case 3:
                    return "回复式";
                case 4:
                    return "干扰式";
                case 5:
                    return "攻击式";
                case 6:
                    return "技巧式";
                case 7:
                    return "速攻式";
                case 8:
                    return "破坏式";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetEffectTypeName(int effectType)
        {
            switch (effectType)
            {
                case 0:
                    return "空";
                case 1:
                    return "Buff";
                case 2:
                    return "Debuff";
                case 3:
                    return "体力回复";
                case 4:
                    return "结界增加";
                case 5:
                    return "灵力上升";
                case 6:
                    return "施加结界异常";
                case 7:
                    return "敌方回合顺序改变";
                case 8:
                    return "己方回合顺序改变";
                case 9:
                    return "恢复结界异常";
                case 10:
                    return "解除禁止状态/强度下降";
                case 11:
                    return "被攻击时减伤";
                case 12:
                    return "受X弹种攻击时伤害下降";
                case 13:
                    return "受X属性攻击时伤害下降";
                case 14:
                    return "受来自X种族攻击时伤害下降";
                case 15:
                    return "X弹种威力上升";
                case 16:
                    return "X属性威力上升";
                case 17:
                    return "灵力回收效率上升";
                case 18:
                    return "换位连携";
                case 19:
                    throw new NotImplementedException();
                case 20:
                    return "对方禁止状态（敌人的）";
                case 21:
                    return "防御式使用时施加Buff";
                case 22:
                    return "支援式使用时施加Buff";
                case 23:
                    return "回复式使用时施加Buff";
                case 24:
                    return "干扰式使用时施加Buff";
                case 25:
                    return "攻击式使用时施加Buff";
                case 26:
                    return "技巧式使用时施加Buff";
                case 27:
                    return "速攻式使用时施加Buff";
                case 28:
                    return "破坏式使用时施加Buff";
                case 29:
                    throw new NotImplementedException();
                case 30:
                    throw new NotImplementedException();
                case 31:
                    return "防御式使用时施加Debuff";
                case 32:
                    return "支援式使用时施加Debuff";
                case 33:
                    return "回复式使用时施加Debuff";
                case 34:
                    return "干扰式使用时施加Debuff";
                case 35:
                    return "攻击式使用时施加Debuff";
                case 36:
                    return "技巧式使用时施加Debuff";
                case 37:
                    return "速攻式使用时施加Debuff";
                case 38:
                    return "破坏式使用时施加Debuff";
                default:
                    throw new NotImplementedException();
            }
        }

        public static Tuple<int, string> GetEffectRemappedInfo(int effectType)
        {
            switch (effectType)
            {
                case 21:  /* "防御式使用时施加Buff" */
                case 22:  /* "支援式使用时施加Buff" */
                case 23:  /* "回复式使用时施加Buff" */
                case 24:  /* "干扰式使用时施加Buff" */
                case 25:  /* "攻击式使用时施加Buff" */
                case 26:  /* "技巧式使用时施加Buff" */
                case 27:  /* "速攻式使用时施加Buff" */
                case 28:  /* "破坏式使用时施加Buff" */
                    return new Tuple<int, string>(1, GetEffectTypeName(1));
                case 31:  /* "防御式使用时施加Debuff" */
                case 32:  /* "支援式使用时施加Debuff" */
                case 33:  /* "回复式使用时施加Debuff" */
                case 34:  /* "干扰式使用时施加Debuff" */
                case 35:  /* "攻击式使用时施加Debuff" */
                case 36:  /* "技巧式使用时施加Debuff" */
                case 37:  /* "速攻式使用时施加Debuff" */
                case 38:  /* "破坏式使用时施加Debuff" */
                    return new Tuple<int, string>(2, GetEffectTypeName(2));
                default:
                    return new Tuple<int, string>(effectType, GetEffectTypeName(effectType));
            }
        }

        public static Tuple<int, string> GetSubEffectRemappedInfo(int effectType, int subEffectType)
        {
            switch (effectType)
            {
                /* Either does not need subeffect id, not does not matter (i.e.case 14) */
                case 0:
                case 3:   /* "体力回复" */
                case 4:   /* "结界增加" */
                case 5:   /* "灵力上升" */
                case 14:  /* "受来自X种族攻击时伤害下降" */
                case 17:  /* "灵力回收效率上升" */
                    return new Tuple<int, string>(0, "空");

                /* Has subtype but not implemented */
                case 6:   /* "施加结界异常" */
                case 7:   /* "敌方回合顺序改变" */
                case 8:   /* "己方回合顺序改变" */
                case 9:   /* "恢复结界异常" */
                case 10:  /* "解除禁止状态/强度下降" */
                case 11:  /* "被攻击时减伤" */
                case 18:  /* "换位连携" */
                case 19:  /* "对方禁止状态（敌人的）" */
                    throw new NotImplementedException();

                case 1:   /* "Buff" */
                case 21:  /* "防御式使用时施加Buff" */
                case 22:  /* "支援式使用时施加Buff" */
                case 23:  /* "回复式使用时施加Buff" */
                case 24:  /* "干扰式使用时施加Buff" */
                case 25:  /* "攻击式使用时施加Buff" */
                case 26:  /* "技巧式使用时施加Buff" */
                case 27:  /* "速攻式使用时施加Buff" */
                case 28:  /* "破坏式使用时施加Buff" */
                    return new Tuple<int, string>(1000 + subEffectType, GetBuffDebuffTypeString(subEffectType, true));

                case 2:   /* "Debuff" */
                case 31:  /* "防御式使用时施加Debuff" */
                case 32:  /* "支援式使用时施加Debuff" */
                case 33:  /* "回复式使用时施加Debuff" */
                case 34:  /* "干扰式使用时施加Debuff" */
                case 35:  /* "攻击式使用时施加Debuff" */
                case 36:  /* "技巧式使用时施加Debuff" */
                case 37:  /* "速攻式使用时施加Debuff" */
                case 38:  /* "破坏式使用时施加Debuff" */
                    return new Tuple<int, string>(2000 + subEffectType, GetBuffDebuffTypeString(subEffectType, false));

                case 12:  /* "受X弹种攻击时伤害下降" */
                case 15:  /* "X弹种威力上升" */
                    return new Tuple<int, string>(3000 + subEffectType, GetBulletTypeStringForSelect(subEffectType));

                case 13:  /* "受X属性攻击时伤害下降" */
                case 16:  /* "X属性威力上升" */
                    return new Tuple<int, string>(4000 + subEffectType, GetElementTypeStringForSelect(subEffectType));

                default:
                    throw new NotImplementedException();
            }
        }

        public static Tuple<int, string> GetRangeRemappedInfo(int rangeType)
        {
            switch (rangeType)
            {
                case 0:  /* "装备者" */
                    return new Tuple<int, string>(1, GetRangeTypeString(1));
                default:
                    return new Tuple<int, string>(rangeType, GetRangeTypeString(rangeType));
            }
        }

        public static Tuple<int, string> GetEffectByRoleRemappedInfo(int effectType)
        {
            switch (effectType)
            {
                case 21:  /* "防御式使用时施加Buff" */
                case 22:  /* "支援式使用时施加Buff" */
                case 23:  /* "回复式使用时施加Buff" */
                case 24:  /* "干扰式使用时施加Buff" */
                case 25:  /* "攻击式使用时施加Buff" */
                case 26:  /* "技巧式使用时施加Buff" */
                case 27:  /* "速攻式使用时施加Buff" */
                case 28:  /* "破坏式使用时施加Buff" */
                    return new Tuple<int, string>(effectType - 20, GetUnitRoleString(effectType - 20));
                case 31:  /* "防御式使用时施加Debuff" */
                case 32:  /* "支援式使用时施加Debuff" */
                case 33:  /* "回复式使用时施加Debuff" */
                case 34:  /* "干扰式使用时施加Debuff" */
                case 35:  /* "攻击式使用时施加Debuff" */
                case 36:  /* "技巧式使用时施加Debuff" */
                case 37:  /* "速攻式使用时施加Debuff" */
                case 38:  /* "破坏式使用时施加Debuff" */
                    return new Tuple<int, string>(effectType - 30, GetUnitRoleString(effectType - 30));
                default:
                    return new Tuple<int, string>(0, "空");
            }
        }

        public static string IsActiveStr(bool isActive)
        {
            return isActive ? "是" : "否";
        }

        public static string CorrectionStr(int correctionType, int correctionValue, int? corrTypeMain, int? corrTypeSub)
        {
            string correctionTypeStr = "";
            switch (correctionType)
            {
                case 1:
                    correctionTypeStr = "体力";
                    break;
                case 2:
                    correctionTypeStr = "阳攻";
                    break;
                case 3:
                    correctionTypeStr = "阳防";
                    break;
                case 4:
                    correctionTypeStr = "阴攻";
                    break;
                case 5:
                    correctionTypeStr = "阴防";
                    break;
                case 6:
                    correctionTypeStr = "速度";
                    break;
                default:
                    throw new NotImplementedException();
            }
            string correctionValueWrapper = Convert.ToString(correctionValue);
            if (corrTypeMain != null && corrTypeMain.GetValueOrDefault() == correctionType)
                correctionValueWrapper = "<b><color=#FF6600>" + correctionValueWrapper + "</color></b>";
            else if (corrTypeSub != null && corrTypeSub.GetValueOrDefault() == correctionType)
                correctionValueWrapper = "<b><color=#4CAFFF>" + correctionValueWrapper + "</color></b>";

            return correctionTypeStr + "+" + correctionValueWrapper;
        }

        public static int CorrectionValueByLevel(int maxValue, int diff, int level)
        {
            return maxValue - diff * (10 - level);
        }

        public static Tuple<int, string> GetTrustCharacteristicName(int type, int subtype)
        {
            switch (type)
            {
                case 18:
                    switch (subtype)
                    {
                        case 1:
                            return new Tuple<int, string>(0, "攻击连携");
                        case 2:
                            return new Tuple<int, string>(1, "防御连携");
                        case 3:
                            return new Tuple<int, string>(2, "速度连携");
                        default:
                            throw new NotImplementedException();
                    }
                case 3:
                    return new Tuple<int, string>(3, "体力连携");
                case 4:
                    return new Tuple<int, string>(4, "屏障连携");
                case 5:
                    return new Tuple<int, string>(5, "灵力连携");
                default:
                    throw new NotImplementedException();
            }
        }


        public static string GetAbnormalBreakString(int bulletAddon)
        {
            switch (bulletAddon)
            {
                case 12:
                    return "火";
                case 13:
                    return "冰";
                case 14:
                    return "电";
                case 15:
                    return "毒";
                case 16:
                    return "暗";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
