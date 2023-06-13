using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnitDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name_kana_sub",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_sub",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "short_name_sub",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name_kana_sub",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "name_sub",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "short_name_sub",
                table: "PlayerUnitData");
        }
    }
}
