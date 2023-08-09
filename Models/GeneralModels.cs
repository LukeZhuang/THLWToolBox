using static THLWToolBox.Helpers.TypeHelper;

namespace THLWToolBox.Models
{
    public class GeneralModels
    {
        public class BulletMagazineModel
        {
            public int bullet_id { get; set; }
            public int bullet_range { get; set; }
            public int bullet_value { get; set; }
            public int bullet_power_rate { get; set; }
            public int boost_count { get; set; }
            // TODO: remove default value for boost_count
            public BulletMagazineModel(int bullet_id, int bullet_range, int bullet_value, int bullet_power_rate, int boost_count=0)
            {
                this.bullet_id = bullet_id;
                this.bullet_range = bullet_range;
                this.bullet_value = bullet_value;
                this.bullet_power_rate = bullet_power_rate;
                this.boost_count = boost_count;
            }
        }

        public class EffectModel
        {
            public int effect_type { get; set; }
            public int sub_effect_type { get; set; }
            public int range { get; set; }
            public int unit_role { get; set; }
            public int turn { get; set; }
            public EffectModel(int effect_type, int sub_effect_type, int range, int unit_role, int turn)
            {
                this.effect_type = effect_type;
                this.sub_effect_type = sub_effect_type;
                this.range = range;
                this.unit_role = unit_role;
                this.turn = turn;
            }
        }

        public class BulletAddonModel
        {
            public int id { get; set; }
            public int value { get; set; }
            public BulletAddonModel(int id, int value)
            {
                this.id = id;
                this.value = value;
            }
        }
    }
}
