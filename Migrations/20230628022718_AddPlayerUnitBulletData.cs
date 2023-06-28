using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerUnitBulletData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUnitBulletData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    element = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    power = table.Column<float>(type: "real", nullable: false),
                    hit = table.Column<int>(type: "int", nullable: false),
                    critical = table.Column<int>(type: "int", nullable: false),
                    bullet1_addon_id = table.Column<int>(type: "int", nullable: false),
                    bullet1_addon_value = table.Column<int>(type: "int", nullable: false),
                    bullet2_addon_id = table.Column<int>(type: "int", nullable: false),
                    bullet2_addon_value = table.Column<int>(type: "int", nullable: false),
                    bullet3_addon_id = table.Column<int>(type: "int", nullable: false),
                    bullet3_addon_value = table.Column<int>(type: "int", nullable: false),
                    bullet1_extraeffect_id = table.Column<int>(type: "int", nullable: false),
                    bullet1_extraeffect_success_rate = table.Column<int>(type: "int", nullable: false),
                    bullet2_extraeffect_id = table.Column<int>(type: "int", nullable: false),
                    bullet2_extraeffect_success_rate = table.Column<int>(type: "int", nullable: false),
                    bullet3_extraeffect_id = table.Column<int>(type: "int", nullable: false),
                    bullet3_extraeffect_success_rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitBulletData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUnitBulletData");
        }
    }
}
