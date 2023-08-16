using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerUnitBulletCriticalRaceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUnitBulletCriticalRaceData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bullet_id = table.Column<int>(type: "int", nullable: false),
                    race_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitBulletCriticalRaceData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUnitBulletCriticalRaceData");
        }
    }
}
