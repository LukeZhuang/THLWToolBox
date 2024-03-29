﻿using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    // Display model definition
    public class UnitShotSpiritRecycleDisplayModel
    {
        public string TypeName { get; set; }
        public string ShotName { get; set; }
        public int Range { get; set; }
        public List<double> BoostRecycles { get; set; }
        public UnitShotSpiritRecycleDisplayModel(string typeName, string shotName, int range, List<double> boostRecycles)
        {
            TypeName = typeName;
            ShotName = shotName;
            Range = range;
            BoostRecycles = boostRecycles;
        }
    }

    public class UnitShotSpiritRecycleHelperViewModel
    {
        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<UnitShotSpiritRecycleDisplayModel>? SpiritRecycleDatas { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public int? EnemyCount { get; set; }
        public int? HitRank { get; set; }
        public int? SourceSmoke { get; set; }
        public int? TargetCharge { get; set; }
        public int? ConfidenceLevel { get; set; }
        public string? DebugString { get; set; }  /* debug info, just in case */


        // Methods
        public static string CreateSpiritRecycleStr(double spiritRecycle)
        {
            return spiritRecycle.ToString("0.00");
        }
    }
}
