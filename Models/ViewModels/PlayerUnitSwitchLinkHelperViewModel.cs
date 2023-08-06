using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class PlayerUnitSwitchLinkHelperViewModel
    {
        public List<Tuple<PlayerUnitData, string>>? QueryUnit { get; set; }
        public List<Tuple<PlayerUnitData, string>>? RelatedUnits { get; set; }
        public string? UnitSymbolName { get; set; }
        public SelectList? SwitchLinkTypes { get; set; }
        public int? SwitchLinkType { get; set; }
    }
}
