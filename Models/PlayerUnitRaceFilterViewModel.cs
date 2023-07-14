using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PlayerUnitRaceDisplayModel
    {
        public PlayerUnitData playerUnitData { get; set; }
        public List<string> queryRaces { get; set; }
        public PlayerUnitRaceDisplayModel(PlayerUnitData playerUnitData, List<string> queryRaces)
        {
            this.playerUnitData = playerUnitData;
            this.queryRaces = queryRaces;
        }
    }
    public class PlayerUnitRaceFilterViewModel
    {
        public List<PlayerUnitRaceDisplayModel> QueryResults { get; set; }
        public string? RaceList { get; set; }
        public string? RaceName { get; set; }
        public string? UnitSymbolName { get; set; }
    }
}
