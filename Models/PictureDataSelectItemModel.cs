namespace THLWToolBox.Models
{
    public class PictureDataSelectItemEffectModel
    {
        public int id { get; set; }
        public string name { get; set; }
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
            this.id = effectType;
            this.name = GeneralTypeMaster.GetEffectTypeName(effectType);
        }
    }
    public class PictureDataSelectItemSubeffectModel
    {
        public int id { get; set; }
        public string name { get; set; }
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
            if (effectType == 0 || subeffectType == 0)
            {
                this.id = 0;
                this.name = "undefined";
            }
            else if (effectType == 14)
            {
                /* "受来自X种族攻击时伤害下降" not implemented */
                this.id = 0;
                this.name = "undefined";
            }
            else
            {
                this.id = GeneralTypeMaster.EncodeSubeffectLowerBound(effectType) + subeffectType;
                this.name = GeneralTypeMaster.GetSubeffectTypeName(effectType, subeffectType);
            }
        }
    }
    public class PictureDataSelectItemRangeModel
    {
        public int id { get; set; }
        public string name { get; set; }
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
            this.id = rangeType;
            this.name = GeneralTypeMaster.GetRangeTypeString(rangeType);
        }
    }
}
