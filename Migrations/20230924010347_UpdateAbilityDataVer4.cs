using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAbilityDataVer4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boost_buff_effect_type",
                table: "PlayerUnitAbilityData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "purge_buff_effect_type",
                table: "PlayerUnitAbilityData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "boost_buff_effect_type",
                table: "PlayerUnitAbilityData");

            migrationBuilder.DropColumn(
                name: "purge_buff_effect_type",
                table: "PlayerUnitAbilityData");
        }
    }
}
