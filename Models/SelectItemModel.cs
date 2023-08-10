using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models
{
    public enum SelectItemTypes
    {
        EffectType = 0,
        SubEffectType = 1,
        RangeType = 2,
        UnitRoleType = 3,
        TurnType = 4,
        SwitchLinkEffectType = 5,
    }
    public class SelectItemModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public SelectItemModel(int id = 0, string name = "")
        {
            this.id = id;
            this.name = name;
        }
        public static SelectItemModel CreateSelectItemForEffect(EffectModel effect, SelectItemTypes type)
        {
            switch (type)
            {
                case SelectItemTypes.EffectType:
                    return GetEffectRemappedInfo(effect.EffectType);
                case SelectItemTypes.SubEffectType:
                    return GetSubEffectRemappedInfo(effect.EffectType, effect.SubEffectType);
                case SelectItemTypes.RangeType:
                    return GetRangeRemappedInfo(effect.Range);
                case SelectItemTypes.UnitRoleType:
                    return GetEffectByRoleRemappedInfo(effect.EffectType);
                case SelectItemTypes.TurnType:
                    return new SelectItemModel(effect.Turn, Convert.ToString(effect.Turn));
                case SelectItemTypes.SwitchLinkEffectType:
                    return GetTrustCharacteristicName(effect.EffectType, effect.SubEffectType);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetBuffDebuffTypeStringForSelect(int effectType, bool isBuff)
        {
            string prefix = isBuff ? "Buff-" : "Debuff-";
            string suffix = isBuff ? "上升" : "下降";
            return prefix + GetBuffDebuffTypeString(effectType) + suffix;
        }

        public static string GetBulletTypeStringForSelect(int elementType)
        {
            return "弹种-" + GetBulletTypeString(elementType) + "弹";
        }

        public static string GetElementTypeStringForSelect(int elementType)
        {
            return "属性-" + GetElementTypeString(elementType);
        }


        // Some effects can be aggregated together, currently only UnitRole related effect types
        public static SelectItemModel GetEffectRemappedInfo(int effectType)
        {
            return effectType switch
            {
                /* "X式使用时施加Buff" */
                21 or 22 or 23 or 24 or 25 or 26 or 27 or 28 => new SelectItemModel(1, GetEffectTypeName(1)),
                /* "X式使用时施加Debuff" */
                31 or 32 or 33 or 34 or 35 or 36 or 37 or 38 => new SelectItemModel(2, GetEffectTypeName(2)),
                _ => new SelectItemModel(effectType, GetEffectTypeName(effectType)),
            };
        }

        // Since subEffects shared a single number space, we need to map them to the same number space.
        // For example, [1000, 2000) means subtypes for buff, [2000, 3000) means subtypes for debuff, e.g.
        public static SelectItemModel GetSubEffectRemappedInfo(int effectType, int subEffectType)
        {
            switch (effectType)
            {
                // Either these ones do not need a subeffect id, or it does not matter (case 14, e.g.)
                case 0:
                case 3:   // "体力回复"
                case 4:   // "结界增加"
                case 5:   // "灵力上升"
                case 14:  // "受来自X种族攻击时伤害下降"
                case 17:  // "灵力回收效率上升"
                    return new SelectItemModel(0, "空");

                // Has subtype but not implemented
                case 6:   // "施加结界异常"
                case 7:   // "敌方回合顺序改变"
                case 8:   // "己方回合顺序改变"
                case 9:   // "恢复结界异常"
                case 10:  // "解除禁止状态/强度下降"
                case 11:  // "被攻击时减伤"
                case 18:  // "换位连携"
                case 19:  // "对方禁止状态（敌人的）"
                    throw new NotImplementedException();

                case 1:   // "Buff"
                case 21:  // "防御式使用时施加Buff"
                case 22:  // "支援式使用时施加Buff"
                case 23:  // "回复式使用时施加Buff"
                case 24:  // "干扰式使用时施加Buff"
                case 25:  // "攻击式使用时施加Buff"
                case 26:  // "技巧式使用时施加Buff"
                case 27:  // "速攻式使用时施加Buff"
                case 28:  // "破坏式使用时施加Buff"
                    return new SelectItemModel(1000 + subEffectType, GetBuffDebuffTypeStringForSelect(subEffectType, true));

                case 2:   // "Debuff"
                case 31:  // "防御式使用时施加Debuff"
                case 32:  // "支援式使用时施加Debuff"
                case 33:  // "回复式使用时施加Debuff"
                case 34:  // "干扰式使用时施加Debuff"
                case 35:  // "攻击式使用时施加Debuff"
                case 36:  // "技巧式使用时施加Debuff"
                case 37:  // "速攻式使用时施加Debuff"
                case 38:  // "破坏式使用时施加Debuff"
                    return new SelectItemModel(2000 + subEffectType, GetBuffDebuffTypeStringForSelect(subEffectType, false));

                case 12:  // "受X弹种攻击时伤害下降"
                case 15:  // "X弹种威力上升"
                    return new SelectItemModel(3000 + subEffectType, GetBulletTypeStringForSelect(subEffectType));

                case 13:  // "受X属性攻击时伤害下降"
                case 16:  // "X属性威力上升"
                    return new SelectItemModel(4000 + subEffectType, GetElementTypeStringForSelect(subEffectType));

                default:
                    throw new NotImplementedException();
            }
        }

        // range type 0 is known as the same as 1
        public static SelectItemModel GetRangeRemappedInfo(int rangeType)
        {
            switch (rangeType)
            {
                case 0:  // "装备者"
                    return new SelectItemModel(1, GetRangeTypeString(1));
                default:
                    return new SelectItemModel(rangeType, GetRangeTypeString(rangeType));
            }
        }

        public static SelectItemModel GetEffectByRoleRemappedInfo(int effectType)
        {
            return effectType switch
            {
                // "X式使用时施加Buff"
                21 or 22 or 23 or 24 or 25 or 26 or 27 or 28 => new SelectItemModel(effectType - 20, GetUnitRoleString(effectType - 20)),
                // "X式使用时施加Debuff"
                31 or 32 or 33 or 34 or 35 or 36 or 37 or 38 => new SelectItemModel(effectType - 30, GetUnitRoleString(effectType - 30)),
                _ => new SelectItemModel(0, "空"),
            };
        }

        public static SelectItemModel GetTrustCharacteristicName(int effectType, int subEffectType)
        {
            return effectType switch
            {
                18 => subEffectType switch
                {
                    1 => new SelectItemModel(0, "攻击连携"),
                    2 => new SelectItemModel(1, "防御连携"),
                    3 => new SelectItemModel(2, "速度连携"),
                    _ => throw new NotImplementedException(),
                },
                3 => new SelectItemModel(3, "体力连携"),
                4 => new SelectItemModel(4, "屏障连携"),
                5 => new SelectItemModel(5, "灵力连携"),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
