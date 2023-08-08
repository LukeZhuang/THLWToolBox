using Microsoft.AspNetCore.Mvc.Rendering;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class UnitRaceHelperViewModel
    {
        // Display model definition
        public class UnitRaceDisplayModel
        {
            public PlayerUnitData unit { get; set; }
            public string races { get; set; }
            public UnitRaceDisplayModel(PlayerUnitData unit, string races)
            {
                this.unit = unit;
                this.races = races;
            }
        }

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
