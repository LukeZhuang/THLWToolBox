using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantFieldsTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "album_id",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "circle_name",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "flavor_text1",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "flavor_text2",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "flavor_text3",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "flavor_text4",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "flavor_text5",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "illustrator_name",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "is_show",
                table: "PictureData");

            migrationBuilder.DropColumn(
                name: "recycle_id",
                table: "PictureData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "album_id",
                table: "PictureData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "circle_name",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flavor_text1",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flavor_text2",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flavor_text3",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flavor_text4",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "flavor_text5",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "illustrator_name",
                table: "PictureData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "is_show",
                table: "PictureData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "recycle_id",
                table: "PictureData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
