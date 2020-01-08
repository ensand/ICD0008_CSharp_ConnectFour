using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleUI;
using Domain;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Pages
{
    public class PlayGame : PageModel
    {
        private readonly DAL.AppDbContext _context;
        public PlayGame(DAL.AppDbContext context) { _context = context; }

        // public Game Game { get; set; } = new Game(new GameSettings());
        // private static readonly GameSettings Settings = new GameSettings();
        public SaveGame SaveGame { get; set; }
        public int GameId { get; set; }
        
        public void OnGet(int? gameId, int? col)
        {
            Console.WriteLine("GameId: " + gameId + ", col: " + col);
            if (gameId != null)
            {
                Console.WriteLine("loaded a game");
                GameId = gameId.Value;
                // Game = _context.SaveGames.Find(gameId);
            } else
            {
                Console.WriteLine("made new game, game id is null");
                SaveGame = new SaveGame()
                {
                    
                };
                // _context.SaveGames.Add();
            }
            
            // if (col != null)
            // {
            //     Game.Move((int) col + 1);
            // }
            //
            // GameUI.PrintBoard(Game);
        }
    }
}