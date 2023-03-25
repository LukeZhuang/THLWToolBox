namespace THLWToolBox.Models
{
    public class GeneralTypeMaster
    {
        public const int SUBEFFECT_RANGE = 1000; 
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
        public static string GetBuffDebuffTypeString(int effectType)
        {
            switch(effectType)
            {
                case 1:
                    return "Buff/Debuff-阳攻";
                case 2:
                    return "Buff/Debuff-阳防";
                case 3:
                    return "Buff/Debuff-阴攻";
                case 4:
                    return "Buff/Debuff-阴防";
                case 5:
                    return "Buff/Debuff-速度";
                case 6:
                    return "Buff/Debuff-命中";
                case 7:
                    return "Buff/Debuff-回避";
                case 8:
                    return "Buff/Debuff-会心攻击";
                case 9:
                    return "Buff/Debuff-会心防御";
                case 10:
                    return "Buff/Debuff-会心命中";
                case 11:
                    return "Buff/Debuff-会心回避";
                case 12:
                    return "Buff/Debuff-仇恨值";
                default:
                    throw new NotImplementedException();
            }
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

        public static int EncodeSubeffectLowerBound(int effectType)
        {
            switch (effectType)
            {
                case 0:
                    return 0;
                case 1:   /* "Buff" */
                case 2:   /* "Debuff" */
                case 21:  /* "防御式使用时施加Buff" */
                case 22:  /* "支援式使用时施加Buff" */
                case 23:  /* "回复式使用时施加Buff" */
                case 24:  /* "干扰式使用时施加Buff" */
                case 25:  /* "攻击式使用时施加Buff" */
                case 26:  /* "技巧式使用时施加Buff" */
                case 27:  /* "速攻式使用时施加Buff" */
                case 28:  /* "破坏式使用时施加Buff" */
                case 31:  /* "防御式使用时施加Debuff" */
                case 32:  /* "支援式使用时施加Debuff" */
                case 33:  /* "回复式使用时施加Debuff" */
                case 34:  /* "干扰式使用时施加Debuff" */
                case 35:  /* "攻击式使用时施加Debuff" */
                case 36:  /* "技巧式使用时施加Debuff" */
                case 37:  /* "速攻式使用时施加Debuff" */
                case 38:  /* "破坏式使用时施加Debuff" */
                    return 1 * SUBEFFECT_RANGE;
                case 12:  /* "受X弹种攻击时伤害下降" */
                case 15:  /* "X弹种威力上升" */
                    return 2 * SUBEFFECT_RANGE;
                case 13:  /* "受X属性攻击时伤害下降" */
                case 16:  /* "X属性威力上升" */
                    return 3 * SUBEFFECT_RANGE;
                default:
                    throw new NotImplementedException();
            }
        }

        /* get encoded subEffectType id and name */
        public static string GetSubeffectTypeName(int effectType, int subEffectType)
        {
            switch (effectType)
            {
                case 0:
                    return "空";
                case 1:   /* "Buff" */
                case 2:   /* "Debuff" */
                case 21:  /* "防御式使用时施加Buff" */
                case 22:  /* "支援式使用时施加Buff" */
                case 23:  /* "回复式使用时施加Buff" */
                case 24:  /* "干扰式使用时施加Buff" */
                case 25:  /* "攻击式使用时施加Buff" */
                case 26:  /* "技巧式使用时施加Buff" */
                case 27:  /* "速攻式使用时施加Buff" */
                case 28:  /* "破坏式使用时施加Buff" */
                case 31:  /* "防御式使用时施加Debuff" */
                case 32:  /* "支援式使用时施加Debuff" */
                case 33:  /* "回复式使用时施加Debuff" */
                case 34:  /* "干扰式使用时施加Debuff" */
                case 35:  /* "攻击式使用时施加Debuff" */
                case 36:  /* "技巧式使用时施加Debuff" */
                case 37:  /* "速攻式使用时施加Debuff" */
                case 38:  /* "破坏式使用时施加Debuff" */
                    return GetBuffDebuffTypeString(subEffectType);
                case 12:  /* "受X弹种攻击时伤害下降" */
                case 15:  /* "X弹种威力上升" */
                    return GetBulletTypeString(subEffectType);
                case 13:  /* "受X属性攻击时伤害下降" */
                case 16:  /* "X属性威力上升" */
                    return GetElementTypeString(subEffectType);
                default:
                    throw new NotImplementedException();
            }
        }

        public static int GetSubeffectDecodedId(int effectType, int encodedSubEffectType)
        {
            try
            {
                int lowerBound = EncodeSubeffectLowerBound(effectType);
                if (encodedSubEffectType >= lowerBound && encodedSubEffectType < lowerBound + SUBEFFECT_RANGE)
                {
                    return encodedSubEffectType - lowerBound;
                }
                else
                {
                    return -1;
                }
            } catch (NotImplementedException e)
            {
                return -1;
            }
        }
    }
}
