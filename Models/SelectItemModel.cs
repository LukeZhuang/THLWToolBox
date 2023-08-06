using static THLWToolBox.Models.GeneralTypeMaster;

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
                    return GetEffectRemappedInfo(effect.effect_type);
                case SelectItemTypes.SubEffectType:
                    return GetSubEffectRemappedInfo(effect.effect_type, effect.sub_effect_type);
                case SelectItemTypes.RangeType:
                    return GetRangeRemappedInfo(effect.range);
                case SelectItemTypes.UnitRoleType:
                    return GetEffectByRoleRemappedInfo(effect.effect_type);
                case SelectItemTypes.TurnType:
                    return new SelectItemModel(effect.turn, Convert.ToString(effect.turn));
                case SelectItemTypes.SwitchLinkEffectType:
                    return GetTrustCharacteristicName(effect.effect_type, effect.sub_effect_type);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
