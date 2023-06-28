namespace THLWToolBox.Models
{
    public class PlayerUnitBulletData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int element { get; set; }
        public int type { get; set; }
        public int category { get; set; }
        public float power { get; set; }
        public int hit { get; set; }
        public int critical { get; set; }
        public int bullet1_addon_id { get; set; }
        public int bullet1_addon_value { get; set; }
        public int bullet2_addon_id { get; set; }
        public int bullet2_addon_value { get; set; }
        public int bullet3_addon_id { get; set; }
        public int bullet3_addon_value { get; set; }
        public int bullet1_extraeffect_id { get; set; }
        public int bullet1_extraeffect_success_rate { get; set; }
        public int bullet2_extraeffect_id { get; set; }
        public int bullet2_extraeffect_success_rate { get; set; }
        public int bullet3_extraeffect_id { get; set; }
        public int bullet3_extraeffect_success_rate { get; set; }
    }
}
