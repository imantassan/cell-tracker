using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CellTracker.Repository.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogRecords",
                columns: table => new
                {
                    Duration = table.Column<int>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CellActionType = table.Column<string>(nullable: false),
                    SubscriberId = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogRecords_SubscriberId_CellActionType",
                table: "LogRecords",
                columns: new[] { "SubscriberId", "CellActionType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogRecords");
        }
    }
}
