using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace THLWToolBox.Models
{
    public class PlayerUnitSwitchLinkHelperViewModel
    {
        public List<PlayerUnitData>? QueryUnit { get; set; }
        public List<PlayerUnitData>? RelatedUnits { get; set; }
        public string? UnitSymbolName { get; set; }
    }
}
