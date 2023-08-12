using Microsoft.AspNetCore.Mvc.Rendering;
using THLWToolBox.Models.DataTypes;
using THLWToolBox.Helpers;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.EffectModel;
using static THLWToolBox.Models.GeneralModels;

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
        public List<EffectSelectBox> CreateEffectSelectBoxes()
        {
            return new()
            {
                new EffectSelectBox(1, EffectId1, SubeffectId1, Range1, UnitRoleTypeId1, TurnTypeId1),
                new EffectSelectBox(2, EffectId2, SubeffectId2, Range2, UnitRoleTypeId2, TurnTypeId2),
            };
        }

        public string CreateCorrectionString(PictureData pictureRecord, int corrId)
        {
            int corrType, corrValue;
            if (corrId == 1)
            {
                corrType = pictureRecord.correction1_type;
                corrValue = pictureRecord.correction1_value;
            }
            else if (corrId == 2)
            {
                corrType = pictureRecord.correction2_type;
                corrValue = pictureRecord.correction2_value;
            }
            else
                throw new InvalidDataException();

            string correctionValueStr = Convert.ToString(corrValue);
            if (CorrTypeMain.GetValueOrDefault(0) == corrType)
                correctionValueStr = "<b><color=#FF6600>" + correctionValueStr + "</color></b>";
            else if (CorrTypeSub.GetValueOrDefault(0) == corrType)
                correctionValueStr = "<b><color=#4CAFFF>" + correctionValueStr + "</color></b>";

            return GetCorrectionTypeString(corrType) + "+" + correctionValueStr;
        }

        public string CreateSimplifiedPictureEffectString(PictureData pictureRecord, bool useMaxValue)
        {
            if (RaceDict == null)
                throw new NullReferenceException();
            if (SimplifiedEffect.GetValueOrDefault(true))
                return string.Join("<ret>", GetEffectModels(pictureRecord, useMaxValue).Select(effect => new SimplifyEffectsHelper(effect, RaceDict).CreateSimplifiedEffectStr()));

            return useMaxValue ? pictureRecord.picture_characteristic_text_max : pictureRecord.picture_characteristic_text;
        }
    }
}
