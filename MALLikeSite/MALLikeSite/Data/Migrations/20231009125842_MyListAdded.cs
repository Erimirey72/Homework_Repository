using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MALLikeSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class MyListAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Titles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_ApplicationUserId",
                table: "Titles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_AspNetUsers_ApplicationUserId",
                table: "Titles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_AspNetUsers_ApplicationUserId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_ApplicationUserId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Titles");
        }
    }
}
