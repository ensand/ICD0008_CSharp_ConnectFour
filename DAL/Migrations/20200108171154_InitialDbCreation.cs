using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<Guid>(nullable: false),
                    BoardString = table.Column<string>(nullable: false),
                    SaveName = table.Column<string>(nullable: true),
                    SaveCreationDateTime = table.Column<string>(nullable: false),
                    BoardHeight = table.Column<int>(nullable: false),
                    BoardWidth = table.Column<int>(nullable: false),
                    PlayerOneMove = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
