using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models
{
    public class EffectModel
    {
        public int EffectType { get; set; }
        public int SubEffectType { get; set; }
        public int Range { get; set; }
        public int UnitRole { get; set; }
        public int Turn { get; set; }
        public int Value { get; set; }
        public EffectModel(int effectType, int subEffectType, int range, int unitRole, int turn, int value)
        {
            EffectType = effectType;
            SubEffectType = subEffectType;
            Range = range;
            UnitRole = unitRole;
            Turn = turn;
            Value = value;
        }

        public static List<EffectModel> GetEffectModels(PictureData pictureRecord, bool useMaxValue)
        {
            return new() {
                new EffectModel(pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_subtype,
                                pictureRecord.picture_characteristic1_effect_range,
                                pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic1_effect_value_max : pictureRecord.picture_characteristic1_effect_value),

                new EffectModel(pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_subtype,
                                pictureRecord.picture_characteristic2_effect_range,
                                pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic2_effect_value_max : pictureRecord.picture_characteristic2_effect_value),

                new EffectModel(pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_subtype,
                                pictureRecord.picture_characteristic3_effect_range,
                                pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic3_effect_value_max : pictureRecord.picture_characteristic3_effect_value),
            };
        }
    }
}
