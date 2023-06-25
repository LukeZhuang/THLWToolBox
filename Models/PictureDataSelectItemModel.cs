namespace THLWToolBox.Models
{
    public class PictureDataSelectItemModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class PictureDataSelectItemEffectModel : PictureDataSelectItemModel
    {
        public PictureDataSelectItemEffectModel(PictureData pd, int effectId)
        {
            int effectType = 0;
            if (effectId == 1)
            {
                effectType = pd.picture_characteristic1_effect_type;
            }
            else if (effectId == 2)
            {
                effectType = pd.picture_characteristic2_effect_type;
            }
            else if (effectId == 3)
            {
                effectType = pd.picture_characteristic3_effect_type;
            }
            else
            {
                throw new NotImplementedException();
            }
            (this.id, this.name) = GeneralTypeMaster.GetEffectRemappedInfo(effectType);
        }
    }
    public class PictureDataSelectItemSubeffectModel : PictureDataSelectItemModel
    {
        public PictureDataSelectItemSubeffectModel(PictureData pd, int effectId)
        {
            int effectType = 0;
            int subeffectType = 0;
            if (effectId == 1)
            {
                effectType = pd.picture_characteristic1_effect_type;
                subeffectType = pd.picture_characteristic1_effect_subtype;
            }
            else if (effectId == 2)
            {
                effectType = pd.picture_characteristic2_effect_type;
                subeffectType = pd.picture_characteristic2_effect_subtype;
            }
            else if (effectId == 3)
            {
                effectType = pd.picture_characteristic3_effect_type;
                subeffectType = pd.picture_characteristic3_effect_subtype;
            }
            else
            {
                throw new NotImplementedException();
            }
            (this.id, this.name) = GeneralTypeMaster.GetSubEffectRemappedInfo(effectType, subeffectType);
        }
    }
    public class PictureDataSelectItemRangeModel : PictureDataSelectItemModel
    {
        public PictureDataSelectItemRangeModel(PictureData pd, int effectId)
        {
            int rangeType = 0;
            if (effectId == 1)
            {
                rangeType = pd.picture_characteristic1_effect_range;
            }
            else if (effectId == 2)
            {
                rangeType = pd.picture_characteristic2_effect_range;
            }
            else if (effectId == 3)
            {
                rangeType = pd.picture_characteristic3_effect_range;
            }
            else
            {
                throw new NotImplementedException();
            }
            (this.id, this.name) = GeneralTypeMaster.GetRangeRemappedInfo(rangeType);
        }
    }

    public class PictureDataSelectItemUnitRoleTypeModel : PictureDataSelectItemModel
    {
        public PictureDataSelectItemUnitRoleTypeModel(PictureData pd, int effectId)
        {
            int effectType = 0;
            if (effectId == 1)
            {
                effectType = pd.picture_characteristic1_effect_type;
            }
            else if (effectId == 2)
            {
                effectType = pd.picture_characteristic2_effect_type;
            }
            else if (effectId == 3)
            {
                effectType = pd.picture_characteristic3_effect_type;
            }
            else
            {
                throw new NotImplementedException();
            }
            (this.id, this.name) = GeneralTypeMaster.GetEffectByRoleRemappedInfo(effectType);
        }
    }

    public class PictureDataSelectItemTurnTypeModel : PictureDataSelectItemModel
    {
        public PictureDataSelectItemTurnTypeModel(PictureData pd, int effectId)
        {
            int turnType = 0;
            if (effectId == 1)
            {
                turnType = pd.picture_characteristic1_effect_turn;
            }
            else if (effectId == 2)
            {
                turnType = pd.picture_characteristic2_effect_turn;
            }
            else if (effectId == 3)
            {
                turnType = pd.picture_characteristic3_effect_turn;
            }
            else
            {
                throw new NotImplementedException();
            }
            (this.id, this.name) = new Tuple<int, string>(turnType, Convert.ToString(turnType));
        }
    }
}
