using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatureAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaceAmenityAndNavProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://i0.wp.com/www.turimexico.com/wp-content/uploads/2015/07/eehierveelagua.jpg?w=747&ssl=1");

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://i0.wp.com/www.turimexico.com/wp-content/uploads/2015/07/eetamul.jpg?w=500&ssl=1");

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Url",
                value: "https://www.gob.mx/cms/uploads/article/main_image/27513/blog_izta_popo.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://upload.wikimedia.org/wikipedia/commons/1/12/Hierve_el_Agua_01.jpg");

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://upload.wikimedia.org/wikipedia/commons/4/4b/Cascada_Tamul.jpg");

            migrationBuilder.UpdateData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Url",
                value: "https://upload.wikimedia.org/wikipedia/commons/7/74/Iztaccihuatl.jpg");
        }
    }
}
