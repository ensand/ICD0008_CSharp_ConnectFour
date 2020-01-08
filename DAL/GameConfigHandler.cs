using System;
using System.Data;
using System.Linq;
using System.Text.Json;
using GameEngine;

namespace DAL
{
    public class GameConfigHandler
    {
        public static void SaveGame(Game game, string? saveName, bool overwrite)
        {
            using (var ctx = new AppDbContext())
            {
                if (overwrite)
                {
                    Game savedGame = ctx.Games.First(g => g.SaveName.Equals(saveName));
                    savedGame.BoardString = JsonSerializer.Serialize(game.GetBoard());
                    savedGame.SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    savedGame.BoardWidth = game.BoardWidth;
                    savedGame.BoardHeight = game.BoardHeight;
                    savedGame.PlayerOneMove = game.PlayerOneMove;

                    ctx.SaveChanges();
                } else
                {
                    game.SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    game.BoardString = JsonSerializer.Serialize(game.GetBoard());
                    game.SaveName = saveName;
                    ctx.Games.Add(game);
                    ctx.SaveChanges();
                }
            }
        }

        public static Game GetGameFromDb(int gameId)
        {
            Game game = null;
            using (var ctx = new AppDbContext())
            {
                foreach (var saveGame in ctx.Games)
                {
                    if (saveGame.GameId == gameId)
                        game = saveGame;
                }

                if (game == null)
                    throw new NoNullAllowedException("No game with id '" + gameId + "' found!");
            }
            
            game.SetBoard(JsonSerializer.Deserialize<int[][]>(game.BoardString));
            return game;
        }
    }
}