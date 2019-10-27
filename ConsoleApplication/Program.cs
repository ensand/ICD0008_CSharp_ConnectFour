using System;
using System.Collections.Generic;
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

            var difficultyMenu = new Menu(2)
            {
                Title = "Select difficulty",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem
                        {
                            Title = "Easy",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "2", new MenuItem
                        {
                            Title = "Moderate",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "3", new MenuItem
                        {
                            Title = "Hard",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "4", new MenuItem
                        {
                            Title = "Extreme",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "5", new MenuItem
                        {
                            Title = "Impossible",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "666", new MenuItem
                        {
                            Title = "Even Satan would not use this in Hell",
                            commandToExecute = TestGame
                        }
                    }
                }
            };
            
            var boardSizesMenu = new Menu(1)
            {
                Title = "Options", 
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem
                        {
                            Title = "Change board height",
                            commandToExecute = ChangeBoardHeight
                        }
                    },
                    {
                        "2", new MenuItem
                        {
                            Title = "Change board width",
                            commandToExecute = ChangeBoardWidth
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
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "2", new MenuItem
                        {
                            Title = "Human starts",
                            commandToExecute = TestGame
                        }
                    },
                    {
                        "3", new MenuItem
                        {
                            Title = "Human against human",
                            commandToExecute = TestGame
                        }
                    }
                }
            };
            
            var loadGameMenu = new Menu(1)
            {
                Title = "Load game",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "S", new MenuItem
                        {
                            Title = "Start game",
                            commandToExecute = TestGame
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
                            commandToExecute = gameMenu.Run
                        }
                    },
                    {
                        "L", new MenuItem
                        {
                            Title = "Load game",
                            commandToExecute = loadGameMenu.Run
                        }
                    },
                    {
                        "O", new MenuItem
                        {
                            Title = "Change board options",
                            commandToExecute = boardSizesMenu.Run
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

        static string TestGame()
        {
            var game = new Game(_settings);
            var done = false;

            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                
                var userXInt = -1;

                do
                {
                    Console.WriteLine("Give me row number.");
                    Console.Write(">");
                    var userInput = Console.ReadLine();
                    
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