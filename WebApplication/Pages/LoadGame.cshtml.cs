using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Pages
{
    public class LoadGame : PageModel
    {
        private readonly DAL.AppDbContext _context;
        public LoadGame(DAL.AppDbContext context) { _context = context; }

        public IList<SaveGame> SavedGames { get;set; } = new List<SaveGame>();

        public async Task<IActionResult> OnGetAsync(int? gameId)
        {
            // if (gameId != null)
            // {
            //     return Redirect("PlayGame?gameId=" + gameId);
            // }
            //
            // SavedGames = await _context.SaveGames.ToListAsync();
            return Page();
        }
    }
}