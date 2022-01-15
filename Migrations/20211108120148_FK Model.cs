using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDailyPost.Migrations
{
    public partial class FKModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_LikeDetails_LikeDetailsId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_LikeDetailsId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "LikeDetailsId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Liked",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "PostDetails",
                table: "LikeDetails",
                newName: "Liked_Post_id");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "LikeDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeDetails_PostId",
                table: "LikeDetails",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDetails_Post_PostId",
                table: "LikeDetails",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeDetails_Post_PostId",
                table: "LikeDetails");

            migrationBuilder.DropIndex(
                name: "IX_LikeDetails_PostId",
                table: "LikeDetails");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "LikeDetails");

            migrationBuilder.RenameColumn(
                name: "Liked_Post_id",
                table: "LikeDetails",
                newName: "PostDetails");

            migrationBuilder.AddColumn<int>(
                name: "LikeDetailsId",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Liked",
                table: "Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Post_LikeDetailsId",
                table: "Post",
                column: "LikeDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_LikeDetails_LikeDetailsId",
                table: "Post",
                column: "LikeDetailsId",
                principalTable: "LikeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
