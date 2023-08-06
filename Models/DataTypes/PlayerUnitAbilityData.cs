namespace THLWToolBox.Models.DataTypes
{
    public class PlayerUnitAbilityData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string resist_ability_description { get; set; }
        public int good_element_take_damage_rate { get; set; }
        public int weak_element_take_damage_rate { get; set; }
        public string element_ability_description { get; set; }
        public int good_element_give_damage_rate { get; set; }
        public int weak_element_give_damage_rate { get; set; }
        public string barrier_ability_description { get; set; }
        public int burning_barrier_type { get; set; }
        public int frozen_barrier_type { get; set; }
        public int electrified_barrier_type { get; set; }
        public int poisoning_barrier_type { get; set; }
        public int blackout_barrier_type { get; set; }
        public string boost_ability_description { get; set; }
        public int boost_power_divergence_type { get; set; }
        public int boost_power_divergence_range { get; set; }
        public string purge_ability_description { get; set; }
        public int purge_barrier_diffusion_type { get; set; }
        public int purge_barrier_diffusion_range { get; set; }
    }
}
