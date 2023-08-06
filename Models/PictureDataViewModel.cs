using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PictureDataViewModel
    {
        public List<PictureData>? DisplayPictureList { get; set; }
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public SelectList? UnitRoleTypes { get; set; }
        public SelectList? TurnTypes { get; set; }
        public int? EffectId1 { get; set; }
        public int? SubeffectId1 { get; set; }
        public int? Range1 { get; set; }
        public int? UnitRoleTypeId1 { get; set; }
        public int? TurnTypeId1 { get; set; }
        public int? EffectId2 { get; set; }
        public int? SubeffectId2 { get; set; }
        public int? Range2 { get; set; }
        public int? UnitRoleTypeId2 { get; set; }
        public int? TurnTypeId2 { get; set; }
        public bool? RareType3 { get; set; }
        public bool? RareType4 { get; set; }
        public bool? RareType5 { get; set; }
        public bool? SimplifiedEffect { get; set; }
        public Dictionary<int, string>? RaceDict { get; set; }
        public int? CorrLevel { get; set; }
        public int? CorrTypeMain { get; set; }
        public int? CorrTypeSub { get; set; }
    }
}
