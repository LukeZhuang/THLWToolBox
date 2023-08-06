using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Data
{
    public class THLWToolBoxContext : DbContext
    {
        public THLWToolBoxContext (DbContextOptions<THLWToolBoxContext> options)
            : base(options)
        {
        }

        public DbSet<PersonRelationData> PersonRelationData { get; set; } = default!;
        public DbSet<PictureData> PictureData { get; set; } = default!;
        public DbSet<PlayerUnitData> PlayerUnitData { get; set; } = default!;
        public DbSet<PlayerUnitCharacteristicData> PlayerUnitCharacteristicData { get; set; } = default!;
        public DbSet<PlayerUnitRaceData> PlayerUnitRaceData { get; set; } = default!;
        public DbSet<PlayerUnitShotData> PlayerUnitShotData { get; set; } = default!;
        public DbSet<PlayerUnitSpellcardData> PlayerUnitSpellcardData { get; set; } = default!;
        public DbSet<RaceData> RaceData { get; set; } = default!;
        public DbSet<VersionHistoryData> VersionHistoryData { get; set; } = default!;
        public DbSet<PlayerUnitBulletData> PlayerUnitBulletData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitBulletCriticalRaceData> PlayerUnitBulletCriticalRaceData { get; set; } = default!;
        public DbSet<PlayerUnitHitCheckOrderData> PlayerUnitHitCheckOrderData { get; set; } = default!;
        public DbSet<PlayerUnitSkillData> PlayerUnitSkillData { get; set; } = default!;
        public DbSet<PlayerUnitSkillEffectData> PlayerUnitSkillEffectData { get; set; } = default!;
        public DbSet<PlayerUnitAbilityData> PlayerUnitAbilityData { get; set; } = default!;
    }
}
