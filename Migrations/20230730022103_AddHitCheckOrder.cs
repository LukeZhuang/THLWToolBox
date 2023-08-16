using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddHitCheckOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerUnitHitCheckOrderData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unit_id = table.Column<int>(type: "int", nullable: false),
                    barrage_id = table.Column<int>(type: "int", nullable: false),
                    boost_id = table.Column<int>(type: "int", nullable: false),
                    hit_check_order = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUnitHitCheckOrderData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerUnitHitCheckOrderData");
        }
    }
}
