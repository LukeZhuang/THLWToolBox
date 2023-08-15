namespace THLWToolBox.Helpers
{
    public class TypeHelper
    {
        public enum BulletAddonType
        {
            AbsHit = 1,
            Penetration = 2,
            Mirror = 3,
            Hard = 4,
            Slash = 5,
            Rapid = 6,
            Blast = 7,
            Elastic = 8,
            Accuracy = 9,
            Recoil = 10,
            Absorb = 11,
            Burn = 12,
            Melt = 13,
            Discharge = 14,
            Drastic = 15,
            Flash = 16,
            BreakBarrier = 17,
            BreakIgnition = 18,
            BreakFreeze = 19,
            BreakCharge = 20,
            BreakBelch = 21,
            BreakSmoke = 22,
        }

        public static string GetRangeTypeString(int range)
        {
            return range switch
            {
                0 => "装备者",
                1 => "自身",
                2 => "己方全体",
                3 => "敌方单体",
                4 => "敌方全体",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetBuffDebuffTypeString(int effectType)
        {
            return effectType switch
            {
                1 => "阳攻",
                2 => "阳防",
                3 => "阴攻",
                4 => "阴防",
                5 => "速度",
                6 => "命中",
                7 => "回避",
                8 => "会心攻击",
                9 => "会心防御",
                10 => "会心命中",
                11 => "会心回避",
                12 => "仇恨值",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetBulletTypeString(int bulletType)
        {
            return bulletType switch
            {
                1 => "通常",
                2 => "镭射",
                3 => "体术",
                4 => "斩击",
                5 => "动能",
                6 => "流体",
                7 => "能量",
                8 => "御符",
                9 => "光",
                10 => "尖头",
                11 => "追踪",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetElementTypeString(int elementType)
        {
            return elementType switch
            {
                1 => "日",
                2 => "月",
                3 => "火",
                4 => "水",
                5 => "木",
                6 => "金",
                7 => "土",
                8 => "星",
                9 => "无",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetUnitRoleString(int unitRole)
        {
            return unitRole switch
            {
                1 => "防御式",
                2 => "支援式",
                3 => "回复式",
                4 => "干扰式",
                5 => "攻击式",
                6 => "技巧式",
                7 => "速攻式",
                8 => "破坏式",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetEffectTypeName(int effectType)
        {
            return effectType switch
            {
                0 => "空",
                1 => "Buff",
                2 => "Debuff",
                3 => "体力回复",
                4 => "结界增加",
                5 => "灵力上升",
                6 => "施加结界异常",
                7 => "敌方回合顺序改变",
                8 => "己方回合顺序改变",
                9 => "恢复结界异常",
                10 => "解除禁止状态/强度下降",
                11 => "被攻击时减伤",
                12 => "受X弹种攻击时伤害下降",
                13 => "受X属性攻击时伤害下降",
                14 => "受来自X种族攻击时伤害下降",
                15 => "X弹种威力上升",
                16 => "X属性威力上升",
                17 => "灵力回收效率上升",
                18 => "换位连携",
                19 => throw new NotImplementedException(),
                20 => "对方禁止状态（敌人的）",
                21 => "防御式使用时施加Buff",
                22 => "支援式使用时施加Buff",
                23 => "回复式使用时施加Buff",
                24 => "干扰式使用时施加Buff",
                25 => "攻击式使用时施加Buff",
                26 => "技巧式使用时施加Buff",
                27 => "速攻式使用时施加Buff",
                28 => "破坏式使用时施加Buff",
                29 => throw new NotImplementedException(),
                30 => throw new NotImplementedException(),
                31 => "防御式使用时施加Debuff",
                32 => "支援式使用时施加Debuff",
                33 => "回复式使用时施加Debuff",
                34 => "干扰式使用时施加Debuff",
                35 => "攻击式使用时施加Debuff",
                36 => "技巧式使用时施加Debuff",
                37 => "速攻式使用时施加Debuff",
                38 => "破坏式使用时施加Debuff",
                41 => "二阶Buff",
                42 => "二阶Debuff",
                _ => throw new NotImplementedException(),
            };
        }


        public static string GetAbnormalBreakString(int bulletAddon)
        {
            return bulletAddon switch
            {
                12 => "火",
                13 => "冰",
                14 => "电",
                15 => "毒",
                16 => "暗",
                _ => throw new InvalidDataException(),
            };
        }

        public static bool IsBreakingAbnormalAddon(int bulletAddon)
        {
            return bulletAddon switch
            {
                12 or 13 or 14 or 15 or 16 => true,
                _ => false,
            };
        }

        public static string GetBulletRangeString(int range)
        {
            return range switch
            {
                1 => "单体",
                2 => "全体",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetCorrectionTypeString(int corrType)
        {
            return corrType switch
            {
                1 => "体力",
                2 => "阳攻",
                3 => "阳防",
                4 => "阴攻",
                5 => "阴防",
                6 => "速度",
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetTimingTypeString(int characteristicType)
        {
            return characteristicType switch
            {
                1 => "Wave开始时，",
                2 => "回合开始时，",
                3 => "被攻击时，",
                4 => "被攻击后，",
                _ => throw new NotImplementedException(),
            };
        }

        public static int GetBarrierTypeBreakingAddonId(int barrierType)
        {
            return barrierType switch
            {
                1 => 12,
                2 => 13,
                3 => 14,
                4 => 15,
                5 => 16,
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetBarrierTypeString(int barrierType)
        {
            return barrierType switch
            {
                1 => "燃烧",
                2 => "冻结",
                3 => "感电",
                4 => "毒雾",
                5 => "黑暗",
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetActOrderChangeTypeString(int actOrderChangeType)
        {
            return actOrderChangeType switch
            {
                1 => "加速",
                2 => "蓄力",
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetEnemyOrderChangeTypeString(int enemyOrderChangeType)
        {
            return enemyOrderChangeType switch
            {
                1 => "击晕",
                2 => "迟缓",
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetDamageReduceTypeString(int damageReduceType)
        {
            return damageReduceType switch
            {
                1 => "减伤",
                2 => "受阳气攻击减伤",
                3 => "受阴气攻击减伤",
                4 => "若满血则减伤",
                5 => "若少于25%血则减伤",
                _ => throw new InvalidDataException(),
            };
        }

        public static string GetBarrierAbilityTypeString(int barrierAbilityType, int barrierStatusType)
        {
            string styleWrapperSt = "<b><font color=#FC0377>";
            string styleWrapperEd = "</font></b>";
            string barrierStatusString = styleWrapperSt + GetBarrierTypeString(barrierStatusType) + styleWrapperEd;
            return barrierAbilityType switch
            {
                1 => "免疫异常[" + barrierStatusString + "]",
                2 => "免疫异常[" + barrierStatusString + "]的负面效果",
                3 => "受到异常[" + barrierStatusString + "]时，对目标施加同样的异常",
                4 => "免疫异常[" + barrierStatusString + "]且体力回复5%",
                5 => "免疫异常[" + barrierStatusString + "]且灵力上升0.2",
                6 => "每有一枚异常[" + barrierStatusString + "]，阳攻/阴攻/会心攻击/会心命中加1级",
                7 => "每有一枚异常[" + barrierStatusString + "]，阳防/阴防/会心防御/会心回避加1级",
                8 => "每有一枚异常[" + barrierStatusString + "]，速度/命中/回避加1级",
                _ => throw new InvalidDataException(),
            };
        }
    }
}
