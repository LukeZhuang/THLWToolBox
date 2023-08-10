using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    // Display model definition
    public class UnitRaceDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public string Races { get; set; }
        public UnitRaceDisplayModel(PlayerUnitData unit, string races)
        {
            this.Unit = unit;
            this.Races = races;
        }
    }

    public class UnitRaceHelperViewModel
    {
        // Display models
        public List<UnitRaceDisplayModel>? QueryUnits { get; set; }
        public List<UnitRaceDisplayModel>? QueryRaces { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public string? RaceName { get; set; }

        // Other Helper Data
        public string? AllRacesStr { get; set; }


        // Methods
        public int GetUnitCountOfRace()
        {
            return QueryRaces == null ? 0 : QueryRaces.Count();
        }
    }
}
