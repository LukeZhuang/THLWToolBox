using System.Text.RegularExpressions;
using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Models
{
    public class EffectModel
    {
        public string Name { get; set; }
        public int EffectType { get; set; }
        public int SubEffectType { get; set; }
        public int Range { get; set; }
        public int UnitRole { get; set; }
        public int Turn { get; set; }
        public int Value { get; set; }
        public int SuccessRate { get; set; }
        public int AddValue { get; set; }
        public string TimingString { get; set; }  // specific to PlayerUnitCharacteristicData
        public string OfficialDescription { get; set; }
        public EffectModel(string name, int effectType, int subEffectType, int range, int unitRole, int turn, int value, int successRate, int addValue, string timingString, string officialDescription)
        {
            Name = name;
            EffectType = effectType;
            SubEffectType = subEffectType;
            Range = range;
            UnitRole = unitRole;
            Turn = turn;
            Value = value;
            SuccessRate = successRate;
            AddValue = addValue;
            TimingString = timingString;
            if (officialDescription.Equals("(empty)"))
                OfficialDescription = "";
            else if (SuccessRate == 0)
                OfficialDescription = new Regex("\\[.*\\]$").Replace(officialDescription, "");
            else
                OfficialDescription = officialDescription;
        }

        public static List<EffectModel> GetEffectModels(PictureData pictureRecord, bool useMaxValue)
        {
            List<string> effectText = (useMaxValue ? pictureRecord.picture_characteristic_text_max : pictureRecord.picture_characteristic_text).Split("<ret>").ToList();
            return new() {
                new EffectModel("",
                                pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_subtype,
                                pictureRecord.picture_characteristic1_effect_range,
                                pictureRecord.picture_characteristic1_effect_type,
                                pictureRecord.picture_characteristic1_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic1_effect_value_max : pictureRecord.picture_characteristic1_effect_value,
                                0,
                                0,
                                "",
                                effectText.Count > 0 ? effectText[0] : ""),

                new EffectModel("",
                                pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_subtype,
                                pictureRecord.picture_characteristic2_effect_range,
                                pictureRecord.picture_characteristic2_effect_type,
                                pictureRecord.picture_characteristic2_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic2_effect_value_max : pictureRecord.picture_characteristic2_effect_value,
                                0,
                                0,
                                "",
                                effectText.Count > 1 ? effectText[1] : ""),

                new EffectModel("",
                                pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_subtype,
                                pictureRecord.picture_characteristic3_effect_range,
                                pictureRecord.picture_characteristic3_effect_type,
                                pictureRecord.picture_characteristic3_effect_turn,
                                useMaxValue ? pictureRecord.picture_characteristic3_effect_value_max : pictureRecord.picture_characteristic3_effect_value,
                                0,
                                0,
                                "",
                                effectText.Count > 2 ? effectText[2] : ""),
            };
        }

        public static EffectModel GetEffectModels(PlayerUnitSkillEffectData skillEffectRecord)
        {
            return new EffectModel(skillEffectRecord.name, skillEffectRecord.type, skillEffectRecord.subtype, skillEffectRecord.range, 0, skillEffectRecord.turn,
                                   skillEffectRecord.level10_value, skillEffectRecord.level10_success_rate, skillEffectRecord.level10_add_value,
                                   "",
                                   string.Format(skillEffectRecord.description,
                                                 skillEffectRecord.type == 5 ? (skillEffectRecord.level10_value / 20.0).ToString("0.00") : skillEffectRecord.level10_value,
                                                 skillEffectRecord.level10_success_rate, skillEffectRecord.level10_add_value));
        }

        public static List<EffectModel> GetEffectModels(PlayerUnitAbilityData abilityRecord)
        {
            return new()
            {
                new EffectModel(abilityRecord.name, abilityRecord.boost_power_divergence_type == 0 ? 0 : 1, abilityRecord.boost_power_divergence_type,
                                abilityRecord.boost_power_divergence_range == 0 ? 1 : 2, 0, 1, 1, 0, 0, "", abilityRecord.boost_ability_description),
                new EffectModel(abilityRecord.name, abilityRecord.purge_barrier_diffusion_range == 0 ? 0 : 1, abilityRecord.purge_barrier_diffusion_type,
                                abilityRecord.purge_barrier_diffusion_range == 0 ? 1 : 2, 0, 1, 1, 0, 0, "", abilityRecord.purge_ability_description),
            };
        }

        public static List<EffectModel> GetEffectModels(PlayerUnitSpellcardData spellcardRecord, Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict)
        {
            List<int> spellcardSkillEffectIds = new()
            {
                spellcardRecord.spellcard_skill1_effect_id,
                spellcardRecord.spellcard_skill2_effect_id,
                spellcardRecord.spellcard_skill3_effect_id,
                spellcardRecord.spellcard_skill4_effect_id,
                spellcardRecord.spellcard_skill5_effect_id,
            };
            return spellcardSkillEffectIds.Where(skillEffectId => skillEffectId != 0).Select(skillEffectId => GetEffectModels(skillEffectDict[skillEffectId])).ToList();
        }

        public static List<EffectModel> GetEffectModels(PlayerUnitSkillData skillRecord, Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict)
        {
            List<int> skillEffectIds = new() { skillRecord.effect1_id, skillRecord.effect2_id, skillRecord.effect3_id };
            return skillEffectIds.Where(skillEffectId => skillEffectId != 0).Select(skillEffectId => GetEffectModels(skillEffectDict[skillEffectId])).ToList();
        }

        public static List<EffectModel> GetEffectModels(PlayerUnitCharacteristicData characteristicRecord)
        {
            return new()
            {
                new EffectModel(characteristicRecord.characteristic1_name,
                                characteristicRecord.characteristic1_effect_type,
                                characteristicRecord.characteristic1_effect_subtype,
                                characteristicRecord.characteristic1_effect_type == 2 || characteristicRecord.characteristic1_effect_type == 6 ? 3 : 1,
                                0,
                                characteristicRecord.characteristic1_effect_type == 1 || characteristicRecord.characteristic1_effect_type == 2 || characteristicRecord.characteristic1_effect_type == 6 ? 3 : 1,
                                characteristicRecord.characteristic1_effect_value,
                                characteristicRecord.characteristic1_rate,
                                0,
                                GetTimingTypeString(characteristicRecord.characteristic1_type),
                                characteristicRecord.characteristic1_description),

                new EffectModel(characteristicRecord.characteristic2_name,
                                characteristicRecord.characteristic2_effect_type,
                                characteristicRecord.characteristic2_effect_subtype,
                                characteristicRecord.characteristic2_effect_type == 2 || characteristicRecord.characteristic2_effect_type == 6 ? 3 : 1,
                                0,
                                characteristicRecord.characteristic2_effect_type == 1 || characteristicRecord.characteristic2_effect_type == 2 || characteristicRecord.characteristic2_effect_type == 6 ? 3 : 1,
                                characteristicRecord.characteristic2_effect_value,
                                characteristicRecord.characteristic2_rate,
                                0,
                                GetTimingTypeString(characteristicRecord.characteristic2_type),
                                characteristicRecord.characteristic2_description),

                new EffectModel(characteristicRecord.characteristic3_name,
                                characteristicRecord.characteristic3_effect_type,
                                characteristicRecord.characteristic3_effect_subtype,
                                characteristicRecord.characteristic3_effect_type == 2 || characteristicRecord.characteristic3_effect_type == 6 ? 3 : 1,
                                0,
                                characteristicRecord.characteristic3_effect_type == 1 || characteristicRecord.characteristic3_effect_type == 2 || characteristicRecord.characteristic3_effect_type == 6 ? 3 : 1,
                                characteristicRecord.characteristic3_effect_value,
                                characteristicRecord.characteristic3_rate,
                                0,
                                GetTimingTypeString(characteristicRecord.characteristic3_type),
                                characteristicRecord.characteristic3_description),
            };
        }
    }
}
