using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceFromGameToDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ParsedDays_ParsedDayId",
                table: "Games");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParsedDayId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ParsedDays_ParsedDayId",
                table: "Games",
                column: "ParsedDayId",
                principalTable: "ParsedDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ParsedDays_ParsedDayId",
                table: "Games");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParsedDayId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ParsedDays_ParsedDayId",
                table: "Games",
                column: "ParsedDayId",
                principalTable: "ParsedDays",
                principalColumn: "Id");
        }
    }
}
