using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Domain;

namespace GameEngine
{
    public class GameConfigHandler
    {
        public static SaveGame GetSaveGame(int[][] board, string saveName)
        {
            return new SaveGame()
            {
                Board = JsonSerializer.Serialize(board),
                PlayerOneMove = false,
                SaveGameName = saveName,
                SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };
        }

        public static Game LoadGameFromDb(string boardString)
        {
            var sb = new StringBuilder(boardString);
            sb.Remove(0, 1);
            sb.Remove(sb.Length-1, 1);
            int height = sb.ToString().Split("[").Length - 1;
            int width = 0;
            for (var i = 0; i < sb.ToString().Length; i++)
            {
                if (sb.ToString()[i].Equals(',') || sb.ToString()[i].Equals('['))
                    continue;

                if (sb.ToString()[i].Equals(']'))
                    break;

                width += 1;
            }

            Console.WriteLine(height + " " + width);
            Game game = new Game(new GameSettings()
            {
                BoardHeight = height,
                BoardWidth = width
            });
            game.LoadGame(JsonSerializer.Deserialize<int[][]>(boardString));
            return game;
        }
    }
}