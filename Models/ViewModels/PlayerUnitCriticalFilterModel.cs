using THLWToolBox.Models.DataTypes;
using THLWToolBox.Models.ViewModels;

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
        public List<UnitRaceDisplayModel>? QueryUnit { get; set; }
        public List<PlayerUnitCriticalDisplayModel> CriticalMatchUnitResults { get; set; }
        public string? RaceList { get; set; }
        public string? RaceName { get; set; }
        public string? UnitSymbolName { get; set; }
        public bool? Shot1 { get; set; }
        public bool? Shot2 { get; set; }
        public bool? NormalSpellcard { get; set; }
        public bool? LastWord { get; set; }



        public static string DisplayShotCriticals(List<Tuple<string, List<string>>> unitBulletList)
        {
            string text = "";
            foreach (var shot in unitBulletList)
            {
                string rowText = "<div class=shot-row>";
                string shotName = shot.Item1;
                rowText += "<div class=shot-name-grid>" + shotName + "</div>";

                rowText += "<div class=critical-bullet-rows>";
                rowText += string.Join("<br/>", shot.Item2);
                rowText += "</div>";

                rowText += "</div>";
                text += rowText;
            }
            return text;
        }
    }
}
