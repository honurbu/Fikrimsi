using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fikrimsi.Repository.Migrations
{
    /// <inheritdoc />
    public partial class relations_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Titles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAppId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_SubjectId",
                table: "Titles",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TitleId",
                table: "Comments",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserAppId",
                table: "Comments",
                column: "UserAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments",
                column: "UserAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Titles_TitleId",
                table: "Comments",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Subjects_SubjectId",
                table: "Titles",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserAppId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Titles_TitleId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Subjects_SubjectId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_SubjectId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TitleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserAppId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserAppId",
                table: "Comments");
        }
    }
}
