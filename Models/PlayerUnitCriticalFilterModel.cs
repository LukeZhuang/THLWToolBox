using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace THLWToolBox.Models
{
    public class PlayerUnitCriticalDisplayModel
    {
        public PlayerUnitData PlayerUnitData { get; set; }
        public List<Tuple<string, List<string>>> unitBulletList { get; set; }
        public double TotalScore { get; set; }
        public PlayerUnitCriticalDisplayModel(PlayerUnitData playerUnitData, List<Tuple<string, List<string>>> unitBulletList, double totalScore)
        {
            this.PlayerUnitData = playerUnitData;
            this.unitBulletList = unitBulletList;
            TotalScore = totalScore;
        }
    }
    public class PlayerUnitCriticalFilterModel
    {
        public List<PlayerUnitCriticalDisplayModel> QueryResults { get; set; }
        public string? RaceList { get; set; }
        public string? RaceName { get; set; }
        public SelectList? Symbols { get; set; }
        public string? UnitName { get; set; }
        public string? SymbolId { get; set; }
        public int? ShotType { get; set; }
    }
}
