using THLWToolBox.Models.DataTypes;

namespace THLWToolBox.Models.ViewModels
{
    public class IdHelperViewModel
    {
        // Display models
        public List<PlayerUnitData>? QueryUnits { get; set; }
        public List<PictureData>? QueryPictures { get; set; }

        // Webpage query Parameters
        public string? UnitSymbolName { get; set; }
        public string? PictureName { get; set; }
    }
}
