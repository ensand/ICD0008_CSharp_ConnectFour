using System;
using System.Collections.Generic;

namespace MenuSystem
{
    public class Menu
    {
        
        /// <summary>
        /// Console menu system and logic
        /// </summary>
        
        private int _menuLevel;
        
        public string Title { get; set; }
        
        private const string MenuCommandExit = "X";
        private const string MenuCommandReturnToPrevious = "P";
        private const string MenuCommandReturnToMain = "M";
        
        private Dictionary<string, MenuItem> _menuItemsDictionary = new Dictionary<string, MenuItem>();

        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }
        
        
        public Dictionary<string, MenuItem> MenuItemsDictionary
        {
            get => _menuItemsDictionary;
            set
            {
                _menuItemsDictionary = value;
                if (_menuLevel >= 1)
                {
                    _menuItemsDictionary.Add("M", new MenuItem() {Title = "Return to main menu"});
                }
                if (_menuLevel >= 2)
                {
                    _menuItemsDictionary.Add("P", new MenuItem() {Title = "Return to previous menu"});
                }
                _menuItemsDictionary.Add("X", new MenuItem() {Title = "Exit"});

            }
        }
        
        public string Run()
        {
            var command = "";
            do
            {
                Console.WriteLine("~~~~ " + Title + " ~~~~");
                
                foreach (var menuItem in MenuItemsDictionary)
                {
                    Console.Write(menuItem.Key);
                    Console.Write(". ");
                    Console.WriteLine(menuItem.Value);
                }

                Console.WriteLine("-------------------");
                Console.Write(">");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";
                Console.Clear();

                var returnCommand = "";
                
                if (MenuItemsDictionary.ContainsKey(command))
                {
                    if (MenuItemsDictionary[command].commandToExecute != null)
                    {
                        returnCommand = MenuItemsDictionary[command].commandToExecute(); // Run the command
                    }
                }
                
                if (returnCommand == MenuCommandExit)
                    command = MenuCommandExit;
                
                if (returnCommand == MenuCommandReturnToMain && _menuLevel != 0)
                    command = MenuCommandReturnToMain;
                
            } while (command != MenuCommandExit && 
                     command != MenuCommandReturnToMain && 
                     command != MenuCommandReturnToPrevious);

            return command;
        }
    }
}