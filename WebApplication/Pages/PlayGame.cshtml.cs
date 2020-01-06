using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages
{
    public class PlayGame : PageModel
    {
        
        public int[][] Board = new int[6][];
        
        public void OnGet()
        {
            for (var i = 0; i < Board.Length; i ++)
            {
                Board[i] = new[] {0, 0, 0, 0, 0, 0, 0, 0};
            }

            Board[5][1] = 1;
            Board[5][2] = 2;
        }
    }
}