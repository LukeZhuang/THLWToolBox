using THLWToolBox.Models.DataTypes;

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
    }
}
