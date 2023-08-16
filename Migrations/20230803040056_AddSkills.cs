using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUnitAbilityData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resist_ability_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    good_element_take_damage_rate = table.Column<int>(type: "int", nullable: false),
                    weak_element_take_damage_rate = table.Column<int>(type: "int", nullable: false),
                    element_ability_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    good_element_give_damage_rate = table.Column<int>(type: "int", nullable: false),
                    weak_element_give_damage_rate = table.Column<int>(type: "int", nullable: false),
                    barrier_ability_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    burning_barrier_type = table.Column<int>(type: "int", nullable: false),
                    frozen_barrier_type = table.Column<int>(type: "int", nullable: false),
                    electrified_barrier_type = table.Column<int>(type: "int", nullable: false),
                    poisoning_barrier_type = table.Column<int>(type: "int", nullable: false),
                    blackout_barrier_type = table.Column<int>(type: "int", nullable: false),
                    boost_ability_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    boost_power_divergence_type = table.Column<int>(type: "int", nullable: false),
                    boost_power_divergence_range = table.Column<int>(type: "int", nullable: false),
                    purge_ability_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    purge_barrier_diffusion_type = table.Column<int>(type: "int", nullable: false),
                    purge_barrier_diffusion_range = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitAbilityData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUnitSkillData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    exp_id = table.Column<int>(type: "int", nullable: false),
                    level1_turn = table.Column<int>(type: "int", nullable: false),
                    level2_turn = table.Column<int>(type: "int", nullable: false),
                    level3_turn = table.Column<int>(type: "int", nullable: false),
                    level4_turn = table.Column<int>(type: "int", nullable: false),
                    level5_turn = table.Column<int>(type: "int", nullable: false),
                    level6_turn = table.Column<int>(type: "int", nullable: false),
                    level7_turn = table.Column<int>(type: "int", nullable: false),
                    level8_turn = table.Column<int>(type: "int", nullable: false),
                    level9_turn = table.Column<int>(type: "int", nullable: false),
                    level10_turn = table.Column<int>(type: "int", nullable: false),
                    effect1_id = table.Column<int>(type: "int", nullable: false),
                    effect1_level_type = table.Column<int>(type: "int", nullable: false),
                    effect1_level_value = table.Column<int>(type: "int", nullable: false),
                    effect2_id = table.Column<int>(type: "int", nullable: false),
                    effect2_level_type = table.Column<int>(type: "int", nullable: false),
                    effect2_level_value = table.Column<int>(type: "int", nullable: false),
                    effect3_id = table.Column<int>(type: "int", nullable: false),
                    effect3_level_type = table.Column<int>(type: "int", nullable: false),
                    effect3_level_value = table.Column<int>(type: "int", nullable: false),
                    icon_filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reincarnation_level_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitSkillData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUnitSkillEffectData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    subtype = table.Column<int>(type: "int", nullable: false),
                    range = table.Column<int>(type: "int", nullable: false),
                    turn = table.Column<int>(type: "int", nullable: false),
                    level1_value = table.Column<int>(type: "int", nullable: false),
                    level1_success_rate = table.Column<int>(type: "int", nullable: false),
                    level1_add_value = table.Column<int>(type: "int", nullable: false),
                    level2_value = table.Column<int>(type: "int", nullable: false),
                    level2_success_rate = table.Column<int>(type: "int", nullable: false),
                    level2_add_value = table.Column<int>(type: "int", nullable: false),
                    level3_value = table.Column<int>(type: "int", nullable: false),
                    level3_success_rate = table.Column<int>(type: "int", nullable: false),
                    level3_add_value = table.Column<int>(type: "int", nullable: false),
                    level4_value = table.Column<int>(type: "int", nullable: false),
                    level4_success_rate = table.Column<int>(type: "int", nullable: false),
                    level4_add_value = table.Column<int>(type: "int", nullable: false),
                    level5_value = table.Column<int>(type: "int", nullable: false),
                    level5_success_rate = table.Column<int>(type: "int", nullable: false),
                    level5_add_value = table.Column<int>(type: "int", nullable: false),
                    level6_value = table.Column<int>(type: "int", nullable: false),
                    level6_success_rate = table.Column<int>(type: "int", nullable: false),
                    level6_add_value = table.Column<int>(type: "int", nullable: false),
                    level7_value = table.Column<int>(type: "int", nullable: false),
                    level7_success_rate = table.Column<int>(type: "int", nullable: false),
                    level7_add_value = table.Column<int>(type: "int", nullable: false),
                    level8_value = table.Column<int>(type: "int", nullable: false),
                    level8_success_rate = table.Column<int>(type: "int", nullable: false),
                    level8_add_value = table.Column<int>(type: "int", nullable: false),
                    level9_value = table.Column<int>(type: "int", nullable: false),
                    level9_success_rate = table.Column<int>(type: "int", nullable: false),
                    level9_add_value = table.Column<int>(type: "int", nullable: false),
                    level10_value = table.Column<int>(type: "int", nullable: false),
                    level10_success_rate = table.Column<int>(type: "int", nullable: false),
                    level10_add_value = table.Column<int>(type: "int", nullable: false),
                    icon_filename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitSkillEffectData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUnitAbilityData");

            migrationBuilder.DropTable(
                name: "PlayerUnitSkillData");

            migrationBuilder.DropTable(
                name: "PlayerUnitSkillEffectData");
        }
    }
}
