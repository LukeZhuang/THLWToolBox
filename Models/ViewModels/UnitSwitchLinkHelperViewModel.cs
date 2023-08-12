using Microsoft.AspNetCore.Mvc.Rendering;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    // Display model definition
    public class UnitSwitchLinkDisplayModel
    {
        public PlayerUnitData Unit { get; set; }
        public string SwitchLinkTypeStr { get; set; }
        public UnitSwitchLinkDisplayModel(PlayerUnitData unit, string switchLinkTypeStr)
        {
            Unit = unit;
            SwitchLinkTypeStr = switchLinkTypeStr;
        }
    }

    public class UnitSwitchLinkHelperViewModel
    {
        // Display models
        public List<UnitSwitchLinkDisplayModel>? QueryUnits { get; set; }
        public List<UnitSwitchLinkDisplayModel>? RelatedUnits { get; set; }

        // Select Lists
        public SelectList? SwitchLinkTypes { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public int? SwitchLinkType { get; set; }
    }
}
