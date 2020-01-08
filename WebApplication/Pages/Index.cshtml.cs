using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger) { _logger = logger; }
        
        [BindProperty]
        [Range(4, 20, ErrorMessage = "Please keep columns in range of {1} to {2}.")]
        public int Columns { get; set; } = 8;
        
        [BindProperty]
        [Range(4, 20, ErrorMessage = "Please keep rows in range of {1} to {2}.")]
        public int Rows { get; set; } = 6;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            return RedirectToPage("PlayGame");
        }
    }
}