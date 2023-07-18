using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace THLWToolBox.Models
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
