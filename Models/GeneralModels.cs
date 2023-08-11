﻿using THLWToolBox.Models.DataTypes;

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
                this.MagazineId = magazineId;
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
