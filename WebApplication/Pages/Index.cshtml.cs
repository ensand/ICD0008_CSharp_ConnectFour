using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DAL.AppDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        
        public GameSettings Settings = new GameSettings();
        
        [BindProperty]
        [Range(4, 10, ErrorMessage = "Please keep columns in range of {1} to {2}.")]
        public int Columns { get; set; } = 8;
        
        [BindProperty]
        [Range(4, 10, ErrorMessage = "Please keep rows in range of {1} to {2}.")]
        public int Rows { get; set; } = 6;

        [BindProperty]
        public bool HumanStarts { get; set; }

        public async void OnGet(Guid? quitGameId)
        {
            if (quitGameId != null)
            {
                var gameToDel = _context.Games.FirstOrDefault(g => g.GameId == quitGameId.Value);
                if (gameToDel != null && gameToDel.SaveName == null)
                {
                    _context.Games.Attach(gameToDel);
                    _context.Games.Remove(gameToDel);
                    await _context.SaveChangesAsync();
                }
            }
        }
        
        public async Task<IActionResult> OnPost(string? error)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Settings.BoardHeight = Rows;
            Settings.BoardWidth = Columns;
            Game game = new Game(Settings);
            game.PlayerOneMove = HumanStarts;
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("PlayGame", new {gameId = game.GameId});
        }
    }
}