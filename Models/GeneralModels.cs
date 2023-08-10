namespace THLWToolBox.Models
{
    public class GeneralModels
    {
        public class BulletMagazineModel
        {
            public int BulletId { get; set; }
            public int BulletRange { get; set; }
            public int BulletValue { get; set; }
            public int BulletPowerRate { get; set; }
            public int BoostCount { get; set; }
            // TODO: remove default value for boost_count
            public BulletMagazineModel(int bulletId, int bulletRange, int bulletValue, int bulletPowerRate, int boostCount = 0)
            {
                this.BulletId = bulletId;
                this.BulletRange = bulletRange;
                this.BulletValue = bulletValue;
                this.BulletPowerRate = bulletPowerRate;
                this.BoostCount = boostCount;
            }
        }

        public class EffectModel
        {
            public int EffectType { get; set; }
            public int SubEffectType { get; set; }
            public int Range { get; set; }
            public int UnitRole { get; set; }
            public int Turn { get; set; }
            public EffectModel(int effectType, int subEffectType, int range, int unitRole, int turn)
            {
                this.EffectType = effectType;
                this.SubEffectType = subEffectType;
                this.Range = range;
                this.UnitRole = unitRole;
                this.Turn = turn;
            }
        }

        public class BulletAddonModel
        {
            public int Id { get; set; }
            public int Value { get; set; }
            public BulletAddonModel(int id, int value)
            {
                this.Id = id;
                this.Value = value;
            }
        }
    }
}
