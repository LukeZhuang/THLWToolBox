using static THLWToolBox.Models.GeneralTypeMaster;

namespace THLWToolBox.Models
{
    public class SelectItemModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class SelectItemModelForEffectType : SelectItemModel
    {
        public SelectItemModelForEffectType(List<EffectModel> allEffects, int index)
        {
            (this.id, this.name) = GeneralTypeMaster.GetEffectRemappedInfo(allEffects[index].effect);
        }
    }

    public class SelectItemModelForSubEffectType : SelectItemModel
    {
        public SelectItemModelForSubEffectType(List<EffectModel> allEffects, int index)
        {
            (this.id, this.name) = GeneralTypeMaster.GetSubEffectRemappedInfo(allEffects[index].effect, allEffects[index].sub_effect);
        }
    }

    public class SelectItemModelForRangeType : SelectItemModel
    {
        public SelectItemModelForRangeType(List<EffectModel> allEffects, int index)
        {
            (this.id, this.name) = GeneralTypeMaster.GetRangeRemappedInfo(allEffects[index].range);
        }
    }

    public class SelectItemModelForUnitRoleType : SelectItemModel
    {
        public SelectItemModelForUnitRoleType(List<EffectModel> allEffects, int index)
        {
            (this.id, this.name) = GeneralTypeMaster.GetEffectByRoleRemappedInfo(allEffects[index].effect);
        }
    }

    public class SelectItemModelForTurnType : SelectItemModel
    {
        public SelectItemModelForTurnType(List<EffectModel> allEffects, int index)
        {
            int turnType = allEffects[index].turn;
            (this.id, this.name) = new Tuple<int, string>(turnType, Convert.ToString(turnType));
        }
    }

    public class SelectItemModelForSwitchLinkEffectType : SelectItemModel
    {
        public SelectItemModelForSwitchLinkEffectType(int effectType, int subEffectType)
        {
            (this.id, this.name) = GeneralTypeMaster.GetTrustCharacteristicName(effectType, subEffectType);
        }
    }
}
