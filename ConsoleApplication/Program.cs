using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleUI;
using DAL;
using GameEngine;
using MenuSystem;

namespace ConsoleApplication
{
    class Program
    {
        private static GameSettings _settings = new GameSettings();
        private static AppDbContext? ctx;
        
        static void Main(string[] args)
        {
            Console.Clear();
        
            var boardSizesMenu = new Menu(1)
            {
                Title = "Options", 
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem
                        {
                            Title = "Change board height",
                            CommandToExecute = ChangeBoardHeight
                        }
                    },
                    {
                        "2", new MenuItem
                        {
                            Title = "Change board width",
                            CommandToExecute = ChangeBoardWidth
                        }
                    }
                }
            };
            
            var gameMenu = new Menu(1)
            {
                Title = "Start a new game of Connect 4", 
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem
                        {
                            Title = "Computer starts",
                            CommandToExecute = RunGame
                        }
                    },
                    {
                        "2", new MenuItem
                        {
                            Title = "Human starts",
                            CommandToExecute = RunGame
                        }
                    }
                }
            };
            
            var mainMenu = new Menu(0)
            {
                Title = "Connect Four", 
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "S", new MenuItem
                        {
                            Title = "Start game",
                            CommandToExecute = RunGame
                        }
                    },
                    {
                        "L", new MenuItem
                        {
                            Title = "Load game",
                            CommandToExecute = LoadGame
                        }
                    },
                    {
                        "O", new MenuItem
                        {
                            Title = "Change board options",
                            CommandToExecute = boardSizesMenu.Run
                        }
                    }
                }
            };
        
            mainMenu.Run();
        }
        
        static string ChangeBoardHeight()
        {
            return ChangeBoardSize("height");
        }
        static string ChangeBoardWidth()
        {
            return ChangeBoardSize("width");
        }
        static String ChangeBoardSize(string settingType)
        {
            var newValue = -1;
            var oldValue = settingType.Equals("height") ? _settings.BoardHeight : _settings.BoardWidth;
        
            do
            {
                Console.Clear();
                Console.WriteLine($"Current {settingType}: {oldValue}");
                Console.WriteLine($"Give me a new {settingType} or type c to cancel");
                Console.Write("> ");
                var input = Console.ReadLine();
        
                if (input == null || input.Trim().Equals(""))
                    continue;
                
                if (input.ToLower().Equals("c"))
                    return "P";
                
                if (!int.TryParse(input, out newValue))
                {
                    Console.WriteLine($"{input} is not a number.");
                }
        
                if (newValue < _settings.MinSize || newValue > _settings.MaxSize)
                {
                    Console.WriteLine($"Please choose a value between {_settings.MinSize} and {_settings.MaxSize}.");
                }
                
            } while (newValue < 0);
        
            if (settingType.Equals("height"))
                _settings.BoardHeight = newValue;
            else if (settingType.Equals("width"))
                _settings.BoardWidth = newValue;
            
            return "P";
        }
        
        static string RunGame()
        {
            StartGame(null);
            return "";
        }
        
        static string LoadGame()
        {
            Console.Clear();
            var savedGamedDictionary = new Dictionary<int, string>();
            
            using (ctx = new AppDbContext())
            {
                var counter = 1;
                foreach (var saveGame in ctx.Games)
                {
                    savedGamedDictionary.Add(counter, saveGame.SaveName);
                    counter += 1;
                }
            }
            
            if (savedGamedDictionary.Count == 0)
            {
                Console.WriteLine("No save files found...");
                Console.WriteLine("Returning to previous menu.");
                Thread.Sleep(3000);
                return "";
            }
            
            Console.WriteLine("Select game by number or type \'c\' to cancel.");
            for (var i = 1; i <= savedGamedDictionary.Count; i++)
            {
                Console.WriteLine(i + ". " + savedGamedDictionary[i]);
            }
            
            var done = false;
            do
            {
                var userInput = Console.ReadLine();

                if (userInput == null || userInput.Trim().Equals("") || userInput.ToLower().Equals("c"))
                    return "";
                
                if (!int.TryParse(userInput, out var selectedGameInt))
                {
                    Console.WriteLine($"{userInput} is not a number.");
                    continue;
                }
            
                if (selectedGameInt <= 0 || savedGamedDictionary.Count < selectedGameInt)
                {
                    Console.WriteLine($"Game {selectedGameInt} does not exist.");
                    continue;
                }
                
                using (ctx = new AppDbContext())
                {
                    foreach (var saveGame in ctx.Games)
                    {
                        if (saveGame.SaveName.Equals(savedGamedDictionary[selectedGameInt]))
                        {
                            Game game = GameConfigHandler.GetGameFromDb(saveGame.GameId);
                            StartGame(game);
                            return "";
                        }
                    }
                    return "";
                }
            
            } while (!done);
        
            return "";

        }
        
        static string StartGame(Game? game)
        {
            if (game == null)
                game = new Game(_settings);
            
            var done = false;
            
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                
                var userXInt = -1;
            
                do
                {
                    Console.WriteLine("Give me row number, or press \'s\' to save game, or press \'q\' to leave game.");
                    Console.Write(">");
                    var userInput = Console.ReadLine();
            
                    if (userInput == null || userInput.Trim().Equals(""))
                        continue;

                    if (userInput.Trim().ToLower().Equals("q"))
                    {
                        userXInt = -1;
                        break;
                    }

                    if (userInput.Trim().ToLower().Equals("s"))
                    {
                        var saved = false;
                        do
                        {
                            Console.WriteLine("Enter save name or press\'c\' to cancel save");
                            Console.Write("> ");
                            var input = Console.ReadLine()?.Trim();
                                
                            if (input == null || input.Equals(""))
                                continue;
            
                            if (input.ToLower().Equals("c"))
                                saved = true;

                            else
                            {
                                Game existingGame;
                                using (ctx = new AppDbContext())
                                {
                                    existingGame = ctx.Games.FirstOrDefault(g => g.SaveName.Equals(input));
                                }

                                if (existingGame != null)
                                {
                                    Console.WriteLine("File already exists. Overwrite it? y/n");
                                    var overwriteHandled = false;
                                    do
                                    {
                                        var response = Console.ReadLine()?.Trim().ToLower();
                                        if (response == null || response.Equals(""))
                                            continue;
                                                    
                                        if (response.Equals("y") || response.Equals("yes"))
                                        {
                                            GameConfigHandler.SaveGame(game, input.Trim(), true);
                                            Console.WriteLine($"Saved game as \'{input}\'!");
                                            saved = true;
                                            overwriteHandled = true;
                                        }
                                                
                                        if (response.Equals("n") || response.Equals("no"))
                                            overwriteHandled = true;
                                                    
                                    } while (!overwriteHandled);
                                } else
                                {
                                    var fileName = input.Trim();
                                    GameConfigHandler.SaveGame(game, fileName, false);
                                    Console.WriteLine($"Saved game as \'{input}\'!");
                                    saved = true;
                                }
                            }
                        } while (!saved);
                        userXInt = -1;
                        continue;
                    }
            
                    if (!int.TryParse(userInput, out userXInt))
                    {
                        Console.WriteLine($"{userInput} is not a number.");
                        userXInt = -1;
                        continue;
                    }
            
                    if (userXInt <= 0 || game.BoardWidth < userXInt)
                    {
                        Console.WriteLine($"Column {userXInt} does not exist.");
                        userXInt = -1;
                        continue;
                    }
            
                    if (game.IsColumnFull(userXInt))
                    {
                        Console.WriteLine($"Column {userXInt} is full.");
                        userXInt = -1;
                    }

                } while (userXInt < 0);

                if (userXInt == -1)
                    break;
                
                game.Move(userXInt);
            
                if (game.IsGameDone())
                {
                    Console.Clear();
                    GameUI.PrintBoard(game);
                    Console.WriteLine("No one won!");
                    done = true;
                }
            
            } while (!done);
            
            return "X";
        }
    }
}