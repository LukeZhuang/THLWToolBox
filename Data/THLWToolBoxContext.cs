using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using THLWToolBox.Models;

namespace THLWToolBox.Data
{
    public class THLWToolBoxContext : DbContext
    {
        public THLWToolBoxContext (DbContextOptions<THLWToolBoxContext> options)
            : base(options)
        {
        }

        public DbSet<THLWToolBox.Models.PersonRelationData> PersonRelationData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PictureData> PictureData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitData> PlayerUnitData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitCharacteristicData> PlayerUnitCharacteristicData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitRaceData> PlayerUnitRaceData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitShotData> PlayerUnitShotData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.PlayerUnitSpellcardData> PlayerUnitSpellcardData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.RaceData> RaceData { get; set; } = default!;
        public DbSet<THLWToolBox.Models.VersionHistoryData> VersionHistoryData { get; set; } = default!;
    }
}
