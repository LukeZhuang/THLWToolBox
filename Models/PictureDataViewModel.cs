using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PictureDataViewModel
    {
        public List<PictureData>? PictureDatas { get; set; }
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public int? EffectId { get; set; }
        public int? SubeffectId { get; set; }
        public int? Range { get; set; }
    }
}
