using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MALLikeSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewNewVoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Staffs_VoiceActorId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_VoiceActorId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "VoiceActorId",
                table: "Characters");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacterId",
                table: "Staffs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CharacterId",
                table: "Staffs",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Characters_CharacterId",
                table: "Staffs",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Characters_CharacterId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_CharacterId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Staffs");

            migrationBuilder.AddColumn<Guid>(
                name: "VoiceActorId",
                table: "Characters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Characters_VoiceActorId",
                table: "Characters",
                column: "VoiceActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Staffs_VoiceActorId",
                table: "Characters",
                column: "VoiceActorId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
