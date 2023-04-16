using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerUnitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUnitData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name_kana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alias_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    short_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    album_id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    exp_id = table.Column<int>(type: "int", nullable: false),
                    symbol_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symbol_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    symbol_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    life_point = table.Column<int>(type: "int", nullable: false),
                    yang_attack = table.Column<int>(type: "int", nullable: false),
                    yang_defense = table.Column<int>(type: "int", nullable: false),
                    yin_attack = table.Column<int>(type: "int", nullable: false),
                    yin_defense = table.Column<int>(type: "int", nullable: false),
                    speed = table.Column<int>(type: "int", nullable: false),
                    shot1_id = table.Column<int>(type: "int", nullable: false),
                    shot2_id = table.Column<int>(type: "int", nullable: false),
                    spellcard1_id = table.Column<int>(type: "int", nullable: false),
                    spellcard2_id = table.Column<int>(type: "int", nullable: false),
                    spellcard3_id = table.Column<int>(type: "int", nullable: false),
                    spellcard4_id = table.Column<int>(type: "int", nullable: false),
                    spellcard5_id = table.Column<int>(type: "int", nullable: false),
                    skill1_id = table.Column<int>(type: "int", nullable: false),
                    skill2_id = table.Column<int>(type: "int", nullable: false),
                    skill3_id = table.Column<int>(type: "int", nullable: false),
                    resist_id = table.Column<int>(type: "int", nullable: false),
                    characteristic_id = table.Column<int>(type: "int", nullable: false),
                    ability_id = table.Column<int>(type: "int", nullable: false),
                    recycle_id = table.Column<int>(type: "int", nullable: false),
                    default_costume_id = table.Column<int>(type: "int", nullable: false),
                    drop_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    limitbreak_item_id = table.Column<int>(type: "int", nullable: false),
                    spellcard_bgm_id = table.Column<int>(type: "int", nullable: false),
                    is_show = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUnitData");
        }
    }
}
