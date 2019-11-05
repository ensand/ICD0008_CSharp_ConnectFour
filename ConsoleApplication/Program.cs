﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace ConsoleApplication
{
    class Program
    {
        private static GameSettings _settings = default!;

        static void Main(string[] args)
        {
            Console.Clear();

            _settings = GameConfigHandler.LoadConfig();

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
                    },
                    {
                        "3", new MenuItem
                        {
                            Title = "Human against human",
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
                            CommandToExecute = gameMenu.Run
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
            
            GameConfigHandler.SaveConfig(_settings);
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
            string[] filePaths = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + "/saves/");
            var savedGamedDictionary = new Dictionary<int, string>();

            if (filePaths.Length == 0)
            {
                Console.WriteLine("No save files found...");
                Console.WriteLine("Returning to previous menu.");
                Thread.Sleep(3000);
                return "";
            }
            
            for (var i = 0; i < filePaths.Length; i++)
            {
                var fileName = filePaths[i].Split("/")[filePaths[i].Split("/").Length - 1].Replace(".json", "");
                savedGamedDictionary.Add((i+1), fileName);
                Console.WriteLine((i+1) + ". " + fileName);
            }

            var done = false;
            do
            {
                Console.WriteLine("Select game by number or type \'c\' to cancel.");
                var userInput = Console.ReadLine();
                var selectedGameInt = -1;

                if (userInput == null || userInput.Trim().Equals("") || userInput.ToLower().Equals("c"))
                    return "";
                
                if (!int.TryParse(userInput, out selectedGameInt))
                {
                    Console.WriteLine($"{userInput} is not a number.");
                    continue;
                }

                if (selectedGameInt <= 0 || savedGamedDictionary.Count < selectedGameInt)
                {
                    Console.WriteLine($"Game {selectedGameInt} does not exist.");
                    continue;
                }

                Game game = GameConfigHandler.LoadGame(filePaths[selectedGameInt-1]);
                StartGame(game);
                return "";

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
                    {
                        userXInt = -1;
                        continue;
                    }
                    
                    if (userInput.Trim().ToLower().Equals("q"))
                    {
                        userXInt = 666;
                        done = true;
                    }
                    else
                    {
                        if (userInput.Trim().ToLower().Equals("s"))
                        {
                            var saved = false;
                            var fileName = "";
                            do
                            {
                                Console.WriteLine("Enter save name or press\'c\' to cancel save");
                                Console.Write("> ");
                                var input = Console.ReadLine();
                                
                                if (input == null || input.Trim().Equals(""))
                                    continue;

                                if (input.Trim().ToLower().Equals("c"))
                                {
                                    saved = true;
                                }
                                else
                                {
                                    if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/saves/" + input + ".json"))
                                    {
                                        Console.WriteLine("File already exists!");
                                        continue;
                                    }

                                    fileName = input.Trim() + ".json";
                                    Console.WriteLine($"Saved game as \'{input}\'!");
                                    GameConfigHandler.SaveGame(game.GetBoard(), fileName);
                                    saved = true;
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
                    }

                } while (userXInt < 0);
                
                if (userXInt != 666)
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