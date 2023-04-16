using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitSwtichLinkAndShot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonRelationData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    target_person_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUnitCharacteristicData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    characteristic1_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic1_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic1_type = table.Column<int>(type: "int", nullable: false),
                    characteristic1_effect_type = table.Column<int>(type: "int", nullable: false),
                    characteristic1_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    characteristic1_rate = table.Column<int>(type: "int", nullable: false),
                    characteristic1_effect_value = table.Column<int>(type: "int", nullable: false),
                    characteristic1_icon_filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic2_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic2_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic2_type = table.Column<int>(type: "int", nullable: false),
                    characteristic2_effect_type = table.Column<int>(type: "int", nullable: false),
                    characteristic2_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    characteristic2_rate = table.Column<int>(type: "int", nullable: false),
                    characteristic2_effect_value = table.Column<int>(type: "int", nullable: false),
                    characteristic2_icon_filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic3_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic3_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    characteristic3_type = table.Column<int>(type: "int", nullable: false),
                    characteristic3_effect_type = table.Column<int>(type: "int", nullable: false),
                    characteristic3_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    characteristic3_rate = table.Column<int>(type: "int", nullable: false),
                    characteristic3_effect_value = table.Column<int>(type: "int", nullable: false),
                    characteristic3_icon_filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trust_characteristic_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trust_characteristic_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trust_characteristic_rear_effect_type = table.Column<int>(type: "int", nullable: false),
                    trust_characteristic_rear_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    trust_characteristic_avent_effect_type = table.Column<int>(type: "int", nullable: false),
                    trust_characteristic_avent_effect_subtype = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitCharacteristicData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUnitShotData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    magazine0_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine1_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine2_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine3_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine4_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine5_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    phantasm_power_up_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level0_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level1_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level2_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level3_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level4_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level5_power_rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitShotData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUnitSpellcardData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine0_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine1_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine1_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine2_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine2_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine3_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine3_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine4_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine4_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    magazine5_boost_count = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_id = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_range = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_value = table.Column<int>(type: "int", nullable: false),
                    magazine5_bullet_power_rate = table.Column<int>(type: "int", nullable: false),
                    phantasm_power_up_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level0_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level1_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level2_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level3_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level4_power_rate = table.Column<int>(type: "int", nullable: false),
                    shot_level5_power_rate = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill1_effect_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill1_level_type = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill1_level_value = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill1_timing = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill2_effect_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill2_level_type = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill2_level_value = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill2_timing = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill3_effect_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill3_level_type = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill3_level_value = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill3_timing = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill4_effect_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill4_level_type = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill4_level_value = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill4_timing = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill5_effect_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill5_level_type = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill5_level_value = table.Column<int>(type: "int", nullable: false),
                    spellcard_skill5_timing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitSpellcardData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelationData");

            migrationBuilder.DropTable(
                name: "PlayerUnitCharacteristicData");

            migrationBuilder.DropTable(
                name: "PlayerUnitShotData");

            migrationBuilder.DropTable(
                name: "PlayerUnitSpellcardData");
        }
    }
}
