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
        
        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }
        public string Title { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public MenuItem MenuItemExit { get; set; } = new MenuItem() {Command = "X", Title = "Exit"};
        public MenuItem MenuItemReturnToPrevious { get; set; } = new MenuItem() {Command = "P", Title = "Return to previous menu"};
        public MenuItem MenuItemReturnToMain { get; set; } = new MenuItem() {Command = "M", Title = "Return to main menu"};

        public string Run()
        {
            var command = "";
            do
            {
                Console.WriteLine("~~~~ " + Title + " ~~~~");
                foreach (var menuItem in MenuItems)
                    Console.WriteLine(menuItem);

                if (_menuLevel >= 2)
                    Console.WriteLine(MenuItemReturnToPrevious);
                if (_menuLevel >= 1)
                    Console.WriteLine(MenuItemReturnToMain);
                
                Console.WriteLine(MenuItemExit);
                Console.WriteLine("-------------------");
                Console.Write("> ");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";
                Console.Clear();

                var returnCommand = "";
                foreach (var menuItem in MenuItems)
                {
                    if (menuItem.Command == command && menuItem.commandToExecute != null)
                    {
                        returnCommand = menuItem.commandToExecute();
                        break;
                    }
                }

                if (returnCommand == MenuItemExit.Command)
                    command = MenuItemExit.Command;
                
                if (returnCommand == MenuItemReturnToMain.Command && _menuLevel != 0)
                    command = MenuItemReturnToMain.Command;

            } while (command != MenuItemExit.Command && 
                     command != MenuItemReturnToMain.Command && 
                     command != MenuItemReturnToPrevious.Command);
            
            return command;
        }
    }
}