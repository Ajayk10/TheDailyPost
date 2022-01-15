using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDailyPost.Migrations.Auth
{
    public partial class UpdatedthesizeofImageUrlfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nVarchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nVarchar(100)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nVarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nVarchar(1000)",
                oldNullable: true);
        }
    }
}
