using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace THLWToolBox.Models
{
    public class GeneralTypeMaster
    {
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
                    return "弹种-通常弹";
                case 2:
                    return "弹种-镭射弹";
                case 3:
                    return "弹种-体术弹";
                case 4:
                    return "弹种-斩击弹";
                case 5:
                    return "弹种-动能弹";
                case 6:
                    return "弹种-流体弹";
                case 7:
                    return "弹种-能量弹";
                case 8:
                    return "弹种-御符弹";
                case 9:
                    return "弹种-光弹";
                case 10:
                    return "弹种-尖头弹";
                case 11:
                    return "弹种-追踪弹";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetElementTypeString(int elementType)
        {
            switch (elementType)
            {
                case 1:
                    return "属性-日";
                case 2:
                    return "属性-月";
                case 3:
                    return "属性-火";
                case 4:
                    return "属性-水";
                case 5:
                    return "属性-木";
                case 6:
                    return "属性-金";
                case 7:
                    return "属性-土";
                case 8:
                    return "属性-星";
                case 9:
                    return "属性-无";
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
                    return new Tuple<int, string>(3000 + subEffectType, GetBulletTypeString(subEffectType));

                case 13:  /* "受X属性攻击时伤害下降" */
                case 16:  /* "X属性威力上升" */
                    return new Tuple<int, string>(4000 + subEffectType, GetElementTypeString(subEffectType));

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
    }
}
