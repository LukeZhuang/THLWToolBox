using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PictureDataViewModel
    {
        public List<PictureData>? PictureDatas { get; set; }
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public SelectList? UnitRoleTypes { get; set; }
        public int? EffectId { get; set; }
        public int? SubeffectId { get; set; }
        public int? Range { get; set; }
        public int? UnitRoleTypeId { get; set; }
        public bool? RareType3 { get; set; }
        public bool? RareType4 { get; set; }
        public bool? RareType5 { get; set; }
        public bool? CorrType1 { get; set; }
        public bool? CorrType2 { get; set; }
        public bool? CorrType3 { get; set; }
        public bool? CorrType4 { get; set; }
        public bool? CorrType5 { get; set; }
        public bool? CorrType6 { get; set; }
        public bool? SimplifiedEffect { get; set; }
        public IDictionary<int, string>? RaceDict { get; set; }
    }
}
