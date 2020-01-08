using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Pages
{
    public class SaveGame : PageModel
    {
        private readonly AppDbContext _context;
        public SaveGame(AppDbContext context) { _context = context; }

        public IList<Game> SavedGames { get;set; } = new List<Game>();

        [BindProperty]
        public bool Overwrite { get; set; }
        
        #nullable enable
        [BindProperty]
        public string? SaveName { get; set; }

        public Guid GameId { get; set; }
        
        public async Task<IActionResult> OnGetAsync(Guid? gameId)
        {
            if (gameId == null)
            {
                return Redirect("Index");
            }
            
            GameId = gameId.Value;

            SavedGames = await _context.Games.Where(g => g.SaveName != null).ToListAsync();
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(Guid? gameId)
        {
            if (!ModelState.IsValid)
                return Page();

            if (gameId == null)
                return Redirect("Index");
            
            GameId = gameId.Value;

            #nullable disable
            var game = _context.Games.AsNoTracking().FirstOrDefault(g => g.GameId.Equals(GameId));

            if (game == null)
                return RedirectToPage("Index", new {error = "game-not-found"});

            if (SaveName == null || SaveName.Trim().Equals("")) return Page();
            {
                var prevSave = _context.Games.AsNoTracking().FirstOrDefault(g => g.SaveName.Equals(SaveName));

                if (prevSave != null && Overwrite)
                {
                    _context.Games.Remove(prevSave);
                }

                if (prevSave != null && !Overwrite) return RedirectToPage("PlayGame", new {gameId = GameId});
                
                game.SaveName = SaveName;
                _context.Games.Update(game);
                await _context.SaveChangesAsync();

                return RedirectToPage("PlayGame", new {gameId = GameId});
            }
        }
    }
}