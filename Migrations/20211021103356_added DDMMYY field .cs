using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDailyPost.Migrations
{
    public partial class addedDDMMYYfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DDMMYY",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDMMYY",
                table: "Post");
        }
    }
}
