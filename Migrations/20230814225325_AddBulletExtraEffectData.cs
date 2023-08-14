using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddBulletExtraEffectData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "VersionHistoryData",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "BulletExtraEffectData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    target = table.Column<int>(type: "int", nullable: false),
                    turn = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    subtype = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulletExtraEffectData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulletExtraEffectData");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "VersionHistoryData",
                newName: "description");
        }
    }
}
