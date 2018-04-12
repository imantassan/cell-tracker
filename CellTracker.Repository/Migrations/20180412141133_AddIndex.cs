using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CellTracker.Repository.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "LogRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CellActionType",
                table: "LogRecords",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_LogRecords_SubscriberId_CellActionType",
                table: "LogRecords",
                columns: new[] { "SubscriberId", "CellActionType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogRecords_SubscriberId_CellActionType",
                table: "LogRecords");

            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "LogRecords",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CellActionType",
                table: "LogRecords",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
