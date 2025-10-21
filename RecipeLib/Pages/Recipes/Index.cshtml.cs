using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeLib.Data;
using RecipeLib.Models;

namespace RecipeLib.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipes { get; set; } = new List<Recipe>();

        public async Task OnGetAsync()
        {
            Recipes = await _context.Recipes.ToListAsync();
        }
    }
}
