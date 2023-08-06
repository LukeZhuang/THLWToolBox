using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class PlayerUnitSpiritRecycleDisplayModel
    {
        public string type_name { get; set; }
        public string shot_name { get; set; }
        public int range { get; set; }
        public List<double> boost_recycles { get; set; }
        public PlayerUnitSpiritRecycleDisplayModel(string type_name, string shot_name, int range, List<double> boost_recycles)
        {
            this.type_name = type_name;
            this.shot_name = shot_name;
            this.range = range;
            this.boost_recycles = boost_recycles;
        }
    }
    public class PlayerUnitSpiritRecycleFilterViewModel
    {
        public List<PlayerUnitData>? QueryUnit { get; set; }
        public List<PlayerUnitSpiritRecycleDisplayModel>? ShotDatas { get; set; }
        public string? UnitSymbolName { get; set; }
        public int? EnemyCount { get; set; }
        public int? HitRank { get; set; }
        public int? SourceSmoke { get; set; }
        public int? TargetCharge { get; set; }
        public int? ConfidenceLevel { get; set; }
    }
}
