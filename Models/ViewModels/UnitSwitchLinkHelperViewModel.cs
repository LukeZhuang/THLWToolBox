using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class UnitSwitchLinkHelperViewModel
    {
        public class UnitsDisplayModel
        {
            public PlayerUnitData unit { get; set; }
            public string SwitchLinkTypeStr { get; set; }
            public UnitsDisplayModel(PlayerUnitData unit, string switchLinkTypeStr)
            {
                this.unit = unit;
                SwitchLinkTypeStr = switchLinkTypeStr;
            }
        }
        public List<UnitsDisplayModel>? QueryUnits { get; set; }
        public List<UnitsDisplayModel>? RelatedUnits { get; set; }
        public SelectList? SwitchLinkTypes { get; set; }
        public string? UnitSymbolName { get; set; }
        public int? SwitchLinkType { get; set; }
    }
}
