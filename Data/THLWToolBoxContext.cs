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

        public DbSet<THLWToolBox.Models.PictureData> PictureData { get; set; } = default!;

        public DbSet<THLWToolBox.Models.PlayerUnitData> PlayerUnitData { get; set; } = default!;
    }
}
