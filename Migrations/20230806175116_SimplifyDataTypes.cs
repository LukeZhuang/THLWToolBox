using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alias_name",
                table: "RaceData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "RaceData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "PlayerUnitSpellcardData");

            migrationBuilder.DropColumn(
                name: "specification",
                table: "PlayerUnitSpellcardData");

            migrationBuilder.DropColumn(
                name: "icon_filename",
                table: "PlayerUnitSkillEffectData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "PlayerUnitSkillData");

            migrationBuilder.DropColumn(
                name: "icon_filename",
                table: "PlayerUnitSkillData");

            migrationBuilder.DropColumn(
                name: "reincarnation_level_name",
                table: "PlayerUnitSkillData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "PlayerUnitShotData");

            migrationBuilder.DropColumn(
                name: "specification",
                table: "PlayerUnitShotData");

            migrationBuilder.DropColumn(
                name: "album_id",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "alias_name",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "default_costume_id",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "drop_text",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "exp_id",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "is_show",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "name_kana",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "name_kana_sub",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "name_sub",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "recycle_id",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "short_name_sub",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "spellcard_bgm_id",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "symbol_description",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "symbol_title",
                table: "PlayerUnitData");

            migrationBuilder.DropColumn(
                name: "characteristic1_icon_filename",
                table: "PlayerUnitCharacteristicData");

            migrationBuilder.DropColumn(
                name: "characteristic2_icon_filename",
                table: "PlayerUnitCharacteristicData");

            migrationBuilder.DropColumn(
                name: "characteristic3_icon_filename",
                table: "PlayerUnitCharacteristicData");

            migrationBuilder.DropColumn(
                name: "trust_characteristic_description",
                table: "PlayerUnitCharacteristicData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "PlayerUnitBulletData");

            migrationBuilder.DropColumn(
                name: "description",
                table: "PlayerUnitAbilityData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alias_name",
                table: "RaceData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "RaceData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PlayerUnitSpellcardData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "specification",
                table: "PlayerUnitSpellcardData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "icon_filename",
                table: "PlayerUnitSkillEffectData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PlayerUnitSkillData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "icon_filename",
                table: "PlayerUnitSkillData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reincarnation_level_name",
                table: "PlayerUnitSkillData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PlayerUnitShotData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "specification",
                table: "PlayerUnitShotData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "album_id",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "alias_name",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "default_costume_id",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "drop_text",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "exp_id",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "is_show",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name_kana",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "recycle_id",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "short_name_sub",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "spellcard_bgm_id",
                table: "PlayerUnitData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "symbol_description",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "symbol_title",
                table: "PlayerUnitData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "characteristic1_icon_filename",
                table: "PlayerUnitCharacteristicData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "characteristic2_icon_filename",
                table: "PlayerUnitCharacteristicData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "characteristic3_icon_filename",
                table: "PlayerUnitCharacteristicData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trust_characteristic_description",
                table: "PlayerUnitCharacteristicData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PlayerUnitBulletData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "PlayerUnitAbilityData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
