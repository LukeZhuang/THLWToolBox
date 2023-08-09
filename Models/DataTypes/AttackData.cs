using static THLWToolBox.Models.GeneralModels;

namespace THLWToolBox.Models.DataTypes
{
    // this class is manually created for generalizing PlayerUnitShotData and PlayerUnitSpellcardData
    public class AttackData
    {
        public string attack_type_name { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<BulletMagazineModel> magazines { get; set; }
        public int phantasm_power_up_rate { get; set; }
        public List<int> power_up_rates { get; set; }

        public AttackData(string attackTypeName, PlayerUnitShotData shotRecord)
        {
            this.attack_type_name = attackTypeName;
            this.id = shotRecord.id;
            this.name = shotRecord.name;
            this.magazines = new()
            {
                new BulletMagazineModel(shotRecord.magazine0_bullet_id, shotRecord.magazine0_bullet_range, shotRecord.magazine0_bullet_value, shotRecord.magazine0_bullet_power_rate, 0),
                new BulletMagazineModel(shotRecord.magazine1_bullet_id, shotRecord.magazine1_bullet_range, shotRecord.magazine1_bullet_value, shotRecord.magazine1_bullet_power_rate, shotRecord.magazine1_boost_count),
                new BulletMagazineModel(shotRecord.magazine2_bullet_id, shotRecord.magazine2_bullet_range, shotRecord.magazine2_bullet_value, shotRecord.magazine2_bullet_power_rate, shotRecord.magazine2_boost_count),
                new BulletMagazineModel(shotRecord.magazine3_bullet_id, shotRecord.magazine3_bullet_range, shotRecord.magazine3_bullet_value, shotRecord.magazine3_bullet_power_rate, shotRecord.magazine3_boost_count),
                new BulletMagazineModel(shotRecord.magazine4_bullet_id, shotRecord.magazine4_bullet_range, shotRecord.magazine4_bullet_value, shotRecord.magazine4_bullet_power_rate, shotRecord.magazine4_boost_count),
                new BulletMagazineModel(shotRecord.magazine5_bullet_id, shotRecord.magazine5_bullet_range, shotRecord.magazine5_bullet_value, shotRecord.magazine5_bullet_power_rate, shotRecord.magazine5_boost_count),
            };
            this.phantasm_power_up_rate = shotRecord.phantasm_power_up_rate;
            this.power_up_rates = new()
            {
                shotRecord.shot_level0_power_rate,
                shotRecord.shot_level1_power_rate,
                shotRecord.shot_level2_power_rate,
                shotRecord.shot_level3_power_rate,
                shotRecord.shot_level4_power_rate,
                shotRecord.shot_level5_power_rate,
            };
        }
        public AttackData(string attackTypeName, PlayerUnitSpellcardData spellcardRecord)
        {
            this.attack_type_name = attackTypeName;
            this.id = spellcardRecord.id;
            this.name = spellcardRecord.name;
            this.magazines = new()
            {
                new BulletMagazineModel(spellcardRecord.magazine0_bullet_id, spellcardRecord.magazine0_bullet_range, spellcardRecord.magazine0_bullet_value, spellcardRecord.magazine0_bullet_power_rate, 0),
                new BulletMagazineModel(spellcardRecord.magazine1_bullet_id, spellcardRecord.magazine1_bullet_range, spellcardRecord.magazine1_bullet_value, spellcardRecord.magazine1_bullet_power_rate, spellcardRecord.magazine1_boost_count),
                new BulletMagazineModel(spellcardRecord.magazine2_bullet_id, spellcardRecord.magazine2_bullet_range, spellcardRecord.magazine2_bullet_value, spellcardRecord.magazine2_bullet_power_rate, spellcardRecord.magazine2_boost_count),
                new BulletMagazineModel(spellcardRecord.magazine3_bullet_id, spellcardRecord.magazine3_bullet_range, spellcardRecord.magazine3_bullet_value, spellcardRecord.magazine3_bullet_power_rate, spellcardRecord.magazine3_boost_count),
                new BulletMagazineModel(spellcardRecord.magazine4_bullet_id, spellcardRecord.magazine4_bullet_range, spellcardRecord.magazine4_bullet_value, spellcardRecord.magazine4_bullet_power_rate, spellcardRecord.magazine4_boost_count),
                new BulletMagazineModel(spellcardRecord.magazine5_bullet_id, spellcardRecord.magazine5_bullet_range, spellcardRecord.magazine5_bullet_value, spellcardRecord.magazine5_bullet_power_rate, spellcardRecord.magazine5_boost_count),
            };
            this.phantasm_power_up_rate = spellcardRecord.phantasm_power_up_rate;
            this.power_up_rates = new()
            {
                spellcardRecord.shot_level0_power_rate,
                spellcardRecord.shot_level1_power_rate,
                spellcardRecord.shot_level2_power_rate,
                spellcardRecord.shot_level3_power_rate,
                spellcardRecord.shot_level4_power_rate,
                spellcardRecord.shot_level5_power_rate,
            };
        }
    }
}
