using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THLWToolBox.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PictureData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    album_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    rare = table.Column<int>(type: "int", nullable: false),
                    illustrator_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    circle_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flavor_text1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flavor_text2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flavor_text3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flavor_text4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flavor_text5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correction1_type = table.Column<int>(type: "int", nullable: false),
                    correction1_value = table.Column<int>(type: "int", nullable: false),
                    correction1_diff = table.Column<int>(type: "int", nullable: false),
                    correction2_type = table.Column<int>(type: "int", nullable: false),
                    correction2_value = table.Column<int>(type: "int", nullable: false),
                    correction2_diff = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_type = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_value = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_value_max = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_turn = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic1_effect_range = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_type = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_value = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_value_max = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_turn = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic2_effect_range = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_type = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_subtype = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_value = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_value_max = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_turn = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic3_effect_range = table.Column<int>(type: "int", nullable: false),
                    picture_characteristic_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    picture_characteristic_text_max = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recycle_id = table.Column<int>(type: "int", nullable: false),
                    is_show = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureData");
        }
    }
}
