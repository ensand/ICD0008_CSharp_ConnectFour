using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    GameStateId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaveGameId = table.Column<int>(nullable: true),
                    Board = table.Column<string>(nullable: true),
                    PlayerOneMove = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.GameStateId);
                });

            migrationBuilder.CreateTable(
                name: "SaveGames",
                columns: table => new
                {
                    SaveGameId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaveGameName = table.Column<string>(nullable: true),
                    Board = table.Column<string>(nullable: true),
                    PlayerOneMove = table.Column<bool>(nullable: false),
                    SaveCreationDateTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveGames", x => x.SaveGameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStates");

            migrationBuilder.DropTable(
                name: "SaveGames");
        }
    }
}
