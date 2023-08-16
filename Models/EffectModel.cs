using System.Linq;
using System.Text.RegularExpressions;
using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Helpers.TypeHelper;
using static THLWToolBox.Models.GeneralModels;

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

        static SkillEffectModel GetMaxLevelSkillEffect(PlayerUnitSkillEffectData skillEffectRecord, SkillLevelTypeModel skillLevelType)
        {
            List<SkillEffectModel> skillEffects = new()
            {
                new SkillEffectModel(skillEffectRecord.level1_value, skillEffectRecord.level1_success_rate, skillEffectRecord.level1_add_value),
                new SkillEffectModel(skillEffectRecord.level2_value, skillEffectRecord.level2_success_rate, skillEffectRecord.level2_add_value),
                new SkillEffectModel(skillEffectRecord.level3_value, skillEffectRecord.level3_success_rate, skillEffectRecord.level3_add_value),
                new SkillEffectModel(skillEffectRecord.level4_value, skillEffectRecord.level4_success_rate, skillEffectRecord.level4_add_value),
                new SkillEffectModel(skillEffectRecord.level5_value, skillEffectRecord.level5_success_rate, skillEffectRecord.level5_add_value),
                new SkillEffectModel(skillEffectRecord.level6_value, skillEffectRecord.level6_success_rate, skillEffectRecord.level6_add_value),
                new SkillEffectModel(skillEffectRecord.level7_value, skillEffectRecord.level7_success_rate, skillEffectRecord.level7_add_value),
                new SkillEffectModel(skillEffectRecord.level8_value, skillEffectRecord.level8_success_rate, skillEffectRecord.level8_add_value),
                new SkillEffectModel(skillEffectRecord.level9_value, skillEffectRecord.level9_success_rate, skillEffectRecord.level9_add_value),
                new SkillEffectModel(skillEffectRecord.level10_value, skillEffectRecord.level10_success_rate, skillEffectRecord.level10_add_value),
            };
            if (skillLevelType.LevelType == 1)
                return skillEffects.First();
            return skillEffects.Last();
        }

        public static EffectModel GetEffectModels(PlayerUnitSkillEffectData skillEffectRecord, SkillLevelTypeModel skillLevelType)
        {
            SkillEffectModel maxLevelSkillEffect = GetMaxLevelSkillEffect(skillEffectRecord, skillLevelType);
            return new EffectModel(skillEffectRecord.name, skillEffectRecord.type, skillEffectRecord.subtype, skillEffectRecord.range, 0, skillEffectRecord.turn,
                                   maxLevelSkillEffect.Value, maxLevelSkillEffect.SuccessRate, maxLevelSkillEffect.AddValue,
                                   "",
                                   string.Format(skillEffectRecord.description,
                                                 skillEffectRecord.type == 5 ? (maxLevelSkillEffect.Value / 20.0).ToString("0.00") : maxLevelSkillEffect.Value,
                                                 maxLevelSkillEffect.SuccessRate, maxLevelSkillEffect.AddValue));
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
            List<SkillLevelTypeModel> spellcardSkillEffectIds = new() {
                new SkillLevelTypeModel(spellcardRecord.spellcard_skill1_effect_id, spellcardRecord.spellcard_skill1_level_type, spellcardRecord.spellcard_skill1_level_value),
                new SkillLevelTypeModel(spellcardRecord.spellcard_skill2_effect_id, spellcardRecord.spellcard_skill2_level_type, spellcardRecord.spellcard_skill2_level_value),
                new SkillLevelTypeModel(spellcardRecord.spellcard_skill3_effect_id, spellcardRecord.spellcard_skill3_level_type, spellcardRecord.spellcard_skill3_level_value),
                new SkillLevelTypeModel(spellcardRecord.spellcard_skill4_effect_id, spellcardRecord.spellcard_skill4_level_type, spellcardRecord.spellcard_skill4_level_value),
                new SkillLevelTypeModel(spellcardRecord.spellcard_skill5_effect_id, spellcardRecord.spellcard_skill5_level_type, spellcardRecord.spellcard_skill5_level_value),
            };
            return spellcardSkillEffectIds.Where(skillEffectId => skillEffectId.EffectId != 0)
                                          .Select(skillEffectId => GetEffectModels(skillEffectDict[skillEffectId.EffectId], skillEffectId)).ToList();
        }

        public static List<EffectModel> GetEffectModels(PlayerUnitSkillData skillRecord, Dictionary<int, PlayerUnitSkillEffectData> skillEffectDict)
        {
            List<SkillLevelTypeModel> skillEffectIds = new() {
                new SkillLevelTypeModel (skillRecord.effect1_id, skillRecord.effect1_level_type, skillRecord.effect1_level_value),
                new SkillLevelTypeModel (skillRecord.effect2_id, skillRecord.effect2_level_type, skillRecord.effect2_level_value),
                new SkillLevelTypeModel (skillRecord.effect3_id, skillRecord.effect3_level_type, skillRecord.effect3_level_value),
            };
            return skillEffectIds.Where(skillEffectId => skillEffectId.EffectId != 0)
                                 .Select(skillEffectId => GetEffectModels(skillEffectDict[skillEffectId.EffectId], skillEffectId)).ToList();
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

        public static List<EffectModel> GetEffectModels(PlayerUnitBulletData bulletRecord, Dictionary<int, BulletExtraEffectData> bulletExtraEffectDict, int attackRange)
        {
            List<EffectModel> effects = new();
            List<BulletExtraEffectModel> bulletExtraEffectModels = new()
            {
                new BulletExtraEffectModel(bulletRecord.bullet1_extraeffect_id, bulletRecord.bullet1_extraeffect_success_rate),
                new BulletExtraEffectModel(bulletRecord.bullet2_extraeffect_id, bulletRecord.bullet2_extraeffect_success_rate),
                new BulletExtraEffectModel(bulletRecord.bullet3_extraeffect_id, bulletRecord.bullet3_extraeffect_success_rate),
            };
            foreach (var bulletExtraEffectModel in bulletExtraEffectModels)
            {
                if (bulletExtraEffectModel.Id == 0)
                    continue;
                BulletExtraEffectData bulletExtraEffect = bulletExtraEffectDict[bulletExtraEffectModel.Id];
                effects.Add(new EffectModel(bulletExtraEffect.name,
                                            bulletExtraEffect.type,
                                            bulletExtraEffect.subtype,
                                            attackRange == 1 ? (/* 单体 */ bulletExtraEffect.target == 1 ? (/* 己方 */ 1) : (/* 敌方 */ 3)) : (/* 全体 */ bulletExtraEffect.target == 1 ? (/* 己方 */ 2) : (/* 敌方 */ 4)),
                                            0,
                                            bulletExtraEffect.turn,
                                            bulletExtraEffect.value,
                                            bulletExtraEffectModel.SuccessRate,
                                            0,
                                            "",
                                            bulletExtraEffect.description));
            }
            return effects;
        }
    }
}
