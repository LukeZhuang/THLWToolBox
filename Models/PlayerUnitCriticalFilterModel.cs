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
        public List<Tuple<PlayerUnitData, string>>? QueryUnit { get; set; }
        public List<PlayerUnitCriticalDisplayModel> CriticalMatchUnitResults { get; set; }
        public string? RaceList { get; set; }
        public string? RaceName { get; set; }
        public string? UnitSymbolName { get; set; }
        public bool? Shot1 { get; set; }
        public bool? Shot2 { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }
    }
}
