using System;
using System.Data;
using System.Linq;
using System.Text.Json;
using GameEngine;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GameConfigHandler
    {
        public static bool SaveGame(Game game, string? saveName, bool overwrite)
        {
            using (var ctx = new AppDbContext())
            {
                // if (ctx.Games.Find(game.GameId) != null && !overwrite)
                // {
                //  todo handle several game copies with unique ids   
                // }
                
                if (ctx.Games.Any(g => g.SaveName == saveName) && !overwrite)
                    return true;

                game.SaveName = saveName;

                if (overwrite)
                {
                    var gameToDel = ctx.Games.FirstOrDefault(g => g.SaveName == saveName);
                    if (gameToDel != null) ctx.Entry((object) gameToDel).State = EntityState.Deleted;
                }

                game.SaveName = saveName;
                game.BoardString = JsonSerializer.Serialize(game.GetBoard());
                game.SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                game.BoardWidth = game.BoardWidth;
                game.BoardHeight = game.BoardHeight;
                game.PlayerOneMove = game.PlayerOneMove;
                
                ctx.Games.Add(game);
                ctx.SaveChanges();
                return false;
            }
        }

        public static Game GetGameFromDb(Guid gameId)
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
            
            // game.SetBoard(JsonSerializer.Deserialize<int[][]>(game.BoardString));
            game.DeserializeBoard(game.BoardString);
            return game;
        }
    }
}