using Microsoft.EntityFrameworkCore.Migrations;
using System;

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
                    Duration = table.Column<TimeSpan>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    CellActionType = table.Column<string>(nullable: false),
                    SubscriberId = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogRecords");
        }
    }
}
