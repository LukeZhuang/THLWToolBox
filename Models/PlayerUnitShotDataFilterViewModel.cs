using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PlayerUnitShotDataDisplayModel
    {
        public PlayerUnitData playerUnitData { get; set; }
        public string shot_name { get; set; }
        public int phantasm_power_up_rate { get; set; }
        public int shot_level0_power_rate { get; set; }
        public int shot_level1_power_rate { get; set; }
        public int shot_level2_power_rate { get; set; }
        public int shot_level3_power_rate { get; set; }
        public int shot_level4_power_rate { get; set; }
        public int shot_level5_power_rate { get; set; }
        public PlayerUnitShotDataDisplayModel(PlayerUnitData playerUnitData, string shot_name, int phantasm_power_up_rate,
                                              int shot_level0_power_rate, int shot_level1_power_rate,
                                              int shot_level2_power_rate, int shot_level3_power_rate,
                                              int shot_level4_power_rate, int shot_level5_power_rate)
        {
            this.playerUnitData = playerUnitData;
            this.shot_name = shot_name;
            this.phantasm_power_up_rate = phantasm_power_up_rate;
            this.shot_level0_power_rate = shot_level0_power_rate;
            this.shot_level1_power_rate = shot_level1_power_rate;
            this.shot_level2_power_rate = shot_level2_power_rate;
            this.shot_level3_power_rate = shot_level3_power_rate;
            this.shot_level4_power_rate = shot_level4_power_rate;
            this.shot_level5_power_rate = shot_level5_power_rate;
        }
    }
    public class PlayerUnitShotDataFilterViewModel
    {
        public List<PlayerUnitShotDataDisplayModel>? PlayerUnitDatas { get; set; }
        public string? UnitSymbolName { get; set; }
        public int? ShotId { get; set; }
    }
}
