using Microsoft.AspNetCore.Mvc.Rendering;
using THLWToolBox.Helpers;
using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Helpers.GeneralHelper;
using static THLWToolBox.Helpers.SimplifyEffectsHelper;

namespace THLWToolBox.Models.ViewModels
{
    public class PictureFilterViewModel
    {
        // Display models
        public List<PictureData>? DisplayPictureList { get; set; }

        // Select Lists
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public SelectList? UnitRoleTypes { get; set; }
        public SelectList? TurnTypes { get; set; }

        // Webpage query Parameters
        public int? EffectId1 { get; set; }
        public int? SubeffectId1 { get; set; }
        public int? Range1 { get; set; }
        public int? UnitRoleTypeId1 { get; set; }
        public int? TurnTypeId1 { get; set; }
        public int? EffectId2 { get; set; }
        public int? SubeffectId2 { get; set; }
        public int? Range2 { get; set; }
        public int? UnitRoleTypeId2 { get; set; }
        public int? TurnTypeId2 { get; set; }
        public bool? RareType3 { get; set; }
        public bool? RareType4 { get; set; }
        public bool? RareType5 { get; set; }
        public bool? SimplifiedEffect { get; set; }
        public int? CorrLevel { get; set; }
        public int? CorrTypeMain { get; set; }
        public int? CorrTypeSub { get; set; }

        // Other Helper Data
        public Dictionary<int, string>? RaceDict { get; set; }


        // Methods
        public static int CorrectionValueByLevel(int maxValue, int diff, int level)
        {
            return maxValue - diff * (10 - level);
        }

        public string CreateCorrectionStr(PictureData pictureRecord, int corrId)
        {
            int corrType, corrValue, corrDiff;
            if (corrId == 1)
            {
                corrType = pictureRecord.correction1_type;
                corrValue = pictureRecord.correction1_value;
                corrDiff = pictureRecord.correction1_diff;
            }
            else if (corrId == 2)
            {
                corrType = pictureRecord.correction2_type;
                corrValue = pictureRecord.correction2_value;
                corrDiff = pictureRecord.correction2_diff;
            }
            else
                throw new NotImplementedException();

            string correctionTypeStr = corrType switch
            {
                1 => "体力",
                2 => "阳攻",
                3 => "阳防",
                4 => "阴攻",
                5 => "阴防",
                6 => "速度",
                _ => throw new NotImplementedException(),
            };

            corrValue = CorrectionValueByLevel(corrValue, corrDiff, CorrLevel.GetValueOrDefault(0));
            string correctionValueStr = Convert.ToString(corrValue);
            if (CorrTypeMain != null && CorrTypeMain.GetValueOrDefault() == corrType)
                correctionValueStr = "<b><color=#FF6600>" + correctionValueStr + "</color></b>";
            else if (CorrTypeSub != null && CorrTypeSub.GetValueOrDefault() == corrType)
                correctionValueStr = "<b><color=#4CAFFF>" + correctionValueStr + "</color></b>";

            return correctionTypeStr + "+" + correctionValueStr;
        }

        public string CreateSimplifiedPictureEffect(PictureData pictureRecord, bool maxEffect)
        {
            if (RaceDict == null)
                throw new NotImplementedException();
            if (SimplifiedEffect.GetValueOrDefault(true))
            {
                List<string> simplifiedEffects = new()
                {
                    CreateSimplifiedEffectStr(pictureRecord.picture_characteristic1_effect_type,
                                              pictureRecord.picture_characteristic1_effect_subtype,
                                              maxEffect ? pictureRecord.picture_characteristic1_effect_value_max : pictureRecord.picture_characteristic1_effect_value,
                                              pictureRecord.picture_characteristic1_effect_turn,
                                              pictureRecord.picture_characteristic1_effect_range,
                                              RaceDict),
                    CreateSimplifiedEffectStr(pictureRecord.picture_characteristic2_effect_type,
                                              pictureRecord.picture_characteristic2_effect_subtype,
                                              maxEffect ? pictureRecord.picture_characteristic2_effect_value_max : pictureRecord.picture_characteristic2_effect_value,
                                              pictureRecord.picture_characteristic2_effect_turn,
                                              pictureRecord.picture_characteristic2_effect_range,
                                              RaceDict),
                    CreateSimplifiedEffectStr(pictureRecord.picture_characteristic3_effect_type,
                                              pictureRecord.picture_characteristic3_effect_subtype,
                                              maxEffect ? pictureRecord.picture_characteristic3_effect_value_max : pictureRecord.picture_characteristic3_effect_value,
                                              pictureRecord.picture_characteristic3_effect_turn,
                                              pictureRecord.picture_characteristic3_effect_range,
                                              RaceDict),
                };
                return string.Join("<ret>", simplifiedEffects);
            }
            return maxEffect ? pictureRecord.picture_characteristic_text_max : pictureRecord.picture_characteristic_text;
        }
    }
}
