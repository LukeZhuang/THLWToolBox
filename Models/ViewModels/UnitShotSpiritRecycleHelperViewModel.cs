using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class UnitShotSpiritRecycleHelperViewModel
    {
        // Display model definition
        public class UnitShotSpiritRecycleHelperDisplayModel
        {
            public string type_name { get; set; }
            public string shot_name { get; set; }
            public int range { get; set; }
            public List<double> boost_recycles { get; set; }
            public UnitShotSpiritRecycleHelperDisplayModel(string type_name, string shot_name, int range, List<double> boost_recycles)
            {
                this.type_name = type_name;
                this.shot_name = shot_name;
                this.range = range;
                this.boost_recycles = boost_recycles;
            }
        }

        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<UnitShotSpiritRecycleHelperDisplayModel>? SpiritRecycleDatas { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public int? EnemyCount { get; set; }
        public int? HitRank { get; set; }
        public int? SourceSmoke { get; set; }
        public int? TargetCharge { get; set; }
        public int? ConfidenceLevel { get; set; }


        // Methods
        public static string CreateSpiritRecycleStr(double spiritRecycle)
        {
            return spiritRecycle.ToString("0.00");
        }
    }
}
