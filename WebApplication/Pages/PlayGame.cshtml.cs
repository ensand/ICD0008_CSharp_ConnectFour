using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleUI;
using DAL;
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

        public Game Game { get; set; }
        public Guid GameId { get; set; }
        
        public async Task<ActionResult> OnGet(Guid? gameId, int? col)
        {
            if (gameId == null)
                return Redirect("Index");

            GameId = gameId.Value;
            
            Game = _context.Games.Find(GameId);
            Game.DeserializeBoard(Game.BoardString);
            Console.WriteLine(Game);
            if (Game == null) return RedirectToPage("Index", new {error = "game-not-found"});

            
            if (col != null)
            {
                int selectedCol = (int) (col + 1);
                if (Game.IsColumnFull(selectedCol))
                {
                    
                }
                else
                {
                    Game.Move((int) col + 1);
                    _context.Games.Update(Game);
                    await _context.SaveChangesAsync();
                }
                
               
            }

            return Page();
        }
    }
}