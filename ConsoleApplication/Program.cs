using System;
using System.Collections.Generic;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace ConsoleApplication
{
    class Program
    {
        private static int BoardHeight { get; set; } = 4;
        private static int BoardWidth { get; set; } = 4;
        private static string PlayerOneColor { get; set; } = "Blue";
        private static string PlayerTwoColor { get; set; } = "Red";
        private static int ArrayStart { get; set; } = 0;
        
        static void Main(string[] args)
        {
            Console.Clear();
            
            var boardSizesMenu = new Menu(2)
            {
                Title = "Options", 
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Command = "1", 
                        Title = "Change board height",
                        commandToExecute = ChangeBoardHeight
                    },
                    new MenuItem
                    {
                        Command = "2", 
                        Title = "Change board width",
                        commandToExecute = ChangeBoardWidth
                    }
                }
            };
            
            var difficultyMenu = new Menu(2)
            {
                Title = "Select difficulty",
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Command = "1", 
                        Title = "Easy",
                        commandToExecute = TestGame
                    },
                    new MenuItem
                    {
                        Command = "2", 
                        Title = "Moderate",
                        commandToExecute = TestGame
                    },
                    new MenuItem
                    {
                        Command = "3", 
                        Title = "Hard",
                        commandToExecute = TestGame
                    },
                    new MenuItem
                    {
                        Command = "4", 
                        Title = "Extreme",
                        commandToExecute = TestGame
                    },
                    new MenuItem
                    {
                        Command = "5", 
                        Title = "Impossible",
                        commandToExecute = TestGame
                    },
                    new MenuItem
                    {
                        Command = "666", 
                        Title = "Even Satan would not use this in Hell",
                        commandToExecute = TestGame
                    }
                }
            };
            
            var gameMenu = new Menu(1)
            {
                Title = "Start a new game of Connect 4", 
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Command = "1", 
                        Title = "Computer starts",
                        commandToExecute = difficultyMenu.Run
                    },
                    new MenuItem
                    {
                        Command = "2", 
                        Title = "Human starts",
                        commandToExecute = difficultyMenu.Run
                    },
                    new MenuItem
                    {
                        Command = "3", 
                        Title = "Human against human",
                        commandToExecute = TestGame
                    }
                }
            };
            
            var optionsMenu = new Menu(1)
            {
                Title = "Options", 
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Command = "1", 
                        Title = "Change board size",
                        commandToExecute = boardSizesMenu.Run
                    },
                    new MenuItem
                    {
                        Command = "2", 
                        Title = "Change players' colours",
                        commandToExecute = null
                    },
                    new MenuItem
                    {
                        Command = "3", 
                        Title = "Change array start number",
                        commandToExecute = null
                    }
                }
            };

            var mainMenu = new Menu(0)
            {
                Title = "Connect Four", 
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Command = "S", 
                        Title = "Start game",
                        commandToExecute = gameMenu.Run
                    },
                    new MenuItem
                    {
                        Command = "O", 
                        Title = "Options",
                        commandToExecute = optionsMenu.Run
                    }
                }
            };

//            mainMenu.Run();
            TestGame();
        }

        static String ChangeBoardHeight()
        {
            var newValue = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("Current height: " + BoardHeight);
                Console.WriteLine("Give me a new height or type c to cancel");
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input.ToLower().Equals("c"))
                    return "P";
                
                if (!int.TryParse(input, out newValue))
                {
                    Console.WriteLine($"{input} is not a number.");
                }
                
            } while (newValue < 0);
            
            BoardHeight = newValue;
            return "P";
        }
        
        static String ChangeBoardWidth()
        {
            var newValue = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("Give me a new width or type c to cancel");
                Console.WriteLine("Current width: " + BoardWidth);
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input.ToLower().Equals("c"))
                    return "P";
                
                if (!int.TryParse(input, out newValue))
                {
                    Console.WriteLine($"{input} is not a number.");
                }
                
            } while (newValue < 0);
            
            BoardWidth = newValue;
            return "P";
        }

        static string TestGame()
        {
            var game = new Game(BoardHeight, BoardWidth);
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
                    
                    if (game.BoardWidth <= userXInt)
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