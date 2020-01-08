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
        public SaveGame SaveGame { get; set; }
        public Game Game { get; set; }
        private static readonly GameSettings Settings = new GameSettings();
        public int GameId { get; set; }
        
        public async void OnGet(int? gameId, int? col)
        {
            // Console.WriteLine("GameId: " + gameId + ", col: " + col);
            // if (gameId != null)
            // {
            //     Console.WriteLine("loaded a game");
            //     GameId = gameId.Value;
            //     SaveGame = _context.SaveGames.Find(gameId);
            //     if (SaveGame == null)
            //     {
            //         Console.WriteLine("game not found. initiated a new game");
            //         Game = new Game(Settings);
            //     } else
            //         Game = GameConfigHandler.LoadGameFromDb(SaveGame.Board);
            // } else
            // {
            //     Console.WriteLine("made new game, game id is null");
            //     var saveGame = new SaveGame()
            //     {
            //         
            //     };
            //     // _context.SaveGames.Add();
            // }
            //
            // if (col != null)
            // {
            //     Game.Move((int) col + 1);
            //     if (SaveGame == null)
            //         SaveGame = new SaveGame();
            //     
            //     SaveGame.PlayerOneMove = Game.PlayerOneMove;
            //     SaveGame.SaveCreationDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //     
            // }
        }
    }
}