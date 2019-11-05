using System;
using System.IO;
using System.Text.Json;

namespace GameEngine
{
    public class GameConfigHandler
    {
        private const string SettingsFileName = "gamesettings.json";
        
        public static void SaveConfig(GameSettings settings, string fileName = SettingsFileName)
        {
            using (var writer = System.IO.File.CreateText(fileName))
            {
                var jsonString = JsonSerializer.Serialize(settings);
                writer.Write(jsonString);
            }
        }

        public static GameSettings LoadConfig(string fileName = SettingsFileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                var res = JsonSerializer.Deserialize<GameSettings>(jsonString);
                
                return res;
            }
            
            return new GameSettings();
        }

        public static void SaveGame(CellState[][] board, string fileName)
        {
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "/saves");
            var path = System.IO.Directory.GetCurrentDirectory() + "/saves/" + fileName;
            using (var writer = System.IO.File.CreateText(path))
            {
                var jsonString = JsonSerializer.Serialize(board);
                writer.Write(jsonString);
            }
        }

        public static Game LoadGame(string fileLocation)
        {
            string jsonString = File.ReadAllText(fileLocation);
            Game game = new Game(LoadConfig());
            game.LoadGame(JsonSerializer.Deserialize<CellState[][]>(jsonString));
            return game;
        }
    }
}