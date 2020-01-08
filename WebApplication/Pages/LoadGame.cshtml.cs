using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Pages
{
    public class LoadGame : PageModel
    {
        private readonly DAL.AppDbContext _context;
        public LoadGame(DAL.AppDbContext context) { _context = context; }

        public IList<Game> SavedGames { get;set; } = new List<Game>();

        public async Task<IActionResult> OnGetAsync(Guid? gameId, Guid? deleteGameId)
        {
            if (gameId != null)
            {
                return Redirect("PlayGame?gameId=" + gameId.Value);
            }

            if (deleteGameId != null)
            {
                var gameToDel = _context.Games.FirstOrDefault(g => g.GameId == deleteGameId.Value);
                if (gameToDel != null)
                {
                    _context.Games.Attach(gameToDel);
                    _context.Games.Remove(gameToDel);
                    await _context.SaveChangesAsync();
                }
            }

            SavedGames = await _context.Games.ToListAsync();
            return Page();
        }
    }
}