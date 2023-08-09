using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    // Display model definition
    public class UnitsDisplayModel
    {
        public PlayerUnitData unit { get; set; }
        public string switchLinkTypeStr { get; set; }
        public UnitsDisplayModel(PlayerUnitData unit, string switchLinkTypeStr)
        {
            this.unit = unit;
            this.switchLinkTypeStr = switchLinkTypeStr;
        }
    }

    public class UnitSwitchLinkHelperViewModel
    {
        // Display models
        public List<UnitsDisplayModel>? QueryUnits { get; set; }
        public List<UnitsDisplayModel>? RelatedUnits { get; set; }

        // Select Lists
        public SelectList? SwitchLinkTypes { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public int? SwitchLinkType { get; set; }
    }
}
