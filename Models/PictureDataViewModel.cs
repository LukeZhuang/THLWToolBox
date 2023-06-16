﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace THLWToolBox.Models
{
    public class PictureDataViewModel
    {
        public List<PictureData>? PictureDatas { get; set; }
        public SelectList? EffectTypes { get; set; }
        public SelectList? SubeffectTypes { get; set; }
        public SelectList? RangeTypes { get; set; }
        public SelectList? UnitRoleTypes { get; set; }
        public SelectList? TurnTypes { get; set; }
        public int? EffectId { get; set; }
        public int? SubeffectId { get; set; }
        public int? Range { get; set; }
        public int? UnitRoleTypeId { get; set; }
        public int? TurnTypeId { get; set; }
        public int? Effect2Id { get; set; }
        public int? Subeffect2Id { get; set; }
        public int? Range2 { get; set; }
        public int? UnitRoleType2Id { get; set; }
        public int? TurnType2Id { get; set; }
        public bool? RareType3 { get; set; }
        public bool? RareType4 { get; set; }
        public bool? RareType5 { get; set; }
        public bool? SimplifiedEffect { get; set; }
        public IDictionary<int, string>? RaceDict { get; set; }
        public int? DisplayPictureLevel { get; set; }
        public int? CorrTypeMain { get; set; }
        public int? CorrTypeSub { get; set; }
    }
}
