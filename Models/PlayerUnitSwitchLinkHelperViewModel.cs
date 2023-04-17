using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace THLWToolBox.Models
{
    public class PlayerUnitSwitchLinkDisplayModel
    {
        public PlayerUnitData playerUnitData { get; set; }
        public List<PlayerUnitData> relatedUnits { get; set; }
        public PlayerUnitSwitchLinkDisplayModel(PlayerUnitData playerUnitData, List<PlayerUnitData> relatedUnits)
        {
            this.playerUnitData = playerUnitData;
            this.relatedUnits = relatedUnits;
        }
    }
    public class PlayerUnitSwitchLinkHelperViewModel
    {
        public List<PlayerUnitSwitchLinkDisplayModel>? PlayerUnitDatas { get; set; }
        public SelectList? Symbols { get; set; }
        public string? UnitName { get; set; }
        public string? SymbolId { get; set; }
    }
}
