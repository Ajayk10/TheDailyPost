using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDailyPost.Migrations
{
    public partial class CommentmodelupdatedwithCommentfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CommentModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CommentModel");
        }
    }
}
