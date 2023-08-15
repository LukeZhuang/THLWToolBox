using THLWToolBox.Models.DataTypes;
using static THLWToolBox.Models.SelectItemModel;

namespace THLWToolBox.Models
{
    public class GeneralModels
    {
        public class BulletMagazineModel
        {
            public int MagazineId { get; set; }
            public int BulletId { get; set; }
            public int BulletRange { get; set; }
            public int BulletValue { get; set; }
            public int BulletPowerRate { get; set; }
            public int BoostCount { get; set; }
            public BulletMagazineModel(int magazineId, int bulletId, int bulletRange, int bulletValue, int bulletPowerRate, int boostCount)
            {
                MagazineId = magazineId;
                BulletId = bulletId;
                BulletRange = bulletRange;
                BulletValue = bulletValue;
                BulletPowerRate = bulletPowerRate;
                BoostCount = boostCount;
            }
        }

        public class BulletAddonModel
        {
            public int Id { get; set; }
            public int Value { get; set; }
            public BulletAddonModel(int id, int value)
            {
                Id = id;
                Value = value;
            }
        }

        public static class AttackScoreWeights
        {
            public static readonly double TypeSpreadShot = 1.0;
            public static readonly double TypeFocusShot = 1.2;
            public static readonly double TypeNormalSpellcard = 3.0;
            public static readonly double TypeLastWord = 5.0;
            public static readonly double RangeSolo = 1.0;
            public static readonly double RangeAll = 1.5;
        }

        public class AttackWithWeightModel
        {
            public AttackData AttackData { get; set; }
            public double AttackWeight { get; set; }
            public AttackWithWeightModel(AttackData attackData, double attackWeight)
            {
                AttackData = attackData;
                AttackWeight = attackWeight;
            }
        }

        public class AttackSelectionModel
        {
            public bool SpreadShot { get; set; }
            public bool FocusShot { get; set; }
            public bool NormalSpellcard { get; set; }
            public bool LastWord { get; set; }
            public AttackSelectionModel(bool spreadShot, bool focusShot, bool normalSpellcard, bool lastWord)
            {
                SpreadShot = spreadShot;
                FocusShot = focusShot;
                NormalSpellcard = normalSpellcard;
                LastWord = lastWord;
            }
        }

        public class EffectSelectBox
        {
            public int BoxId { get; set; }
            public int? EffectId { get; set; }
            public int? SubeffectId { get; set; }
            public List<int?> Ranges { get; set; }  // All allowed rangeIds
            public int? UnitRoleTypeId { get; set; }
            public int? TurnTypeId { get; set; }
            public EffectSelectBox(int boxId, int? effectId, int? subeffectId, List<int?> ranges, int? unitRoleTypeId, int? turnTypeId)
            {
                BoxId = boxId;
                EffectId = effectId;
                SubeffectId = subeffectId;
                Ranges = ranges;
                UnitRoleTypeId = unitRoleTypeId;
                TurnTypeId = turnTypeId;
            }
            public bool IsEffectiveSelectBox()
            {
                return EffectId != null;
            }

            bool EffectMatchesSelectBox(EffectModel effect)
            {
                if (!IsEffectiveSelectBox())
                    return true;

                if (EffectId != null && EffectId.GetValueOrDefault() != CreateSelectItemForEffect(effect, SelectItemTypes.EffectType).id)
                    return false;
                if (SubeffectId != null && SubeffectId.GetValueOrDefault() != CreateSelectItemForEffect(effect, SelectItemTypes.SubEffectType).id)
                    return false;
                if (Ranges.Where(x => x != null).Any() && !Ranges.Where(x => x != null).Cast<int>().Contains(CreateSelectItemForEffect(effect, SelectItemTypes.RangeType).id))
                    return false;
                if (UnitRoleTypeId != null && UnitRoleTypeId.GetValueOrDefault() != CreateSelectItemForEffect(effect, SelectItemTypes.UnitRoleType).id)
                    return false;
                if (TurnTypeId != null && TurnTypeId.GetValueOrDefault() != CreateSelectItemForEffect(effect, SelectItemTypes.TurnType).id)
                    return false;

                return true;
            }

            public bool EffectListMatchesSelectBox(List<EffectModel> effects)
            {
                if (!IsEffectiveSelectBox())
                    return true;
                // return true if any effect in the list matches this SelectBox
                return effects.Select(EffectMatchesSelectBox).Any(x => x);
            }
        }

        public class SkillEffectInfo
        {
            public int Type { get; set; }
            public string SubType { get; set; }
            public List<EffectModel> Effects { get; set; }
            public SkillEffectInfo(int type, string subType, List<EffectModel> effects)
            {
                Type = type;
                SubType = subType;
                Effects = effects;
            }
        }

        public class BulletExtraEffectModel
        {
            public int Id { get; set; }
            public int SuccessRate { get; set; }
            public BulletExtraEffectModel(int id, int successRate)
            {
                Id = id;
                SuccessRate = successRate;
            }
        }
    }
}
