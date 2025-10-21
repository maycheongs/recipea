using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeLib.Data;
using RecipeLib.Models;

namespace RecipeLib.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly RecipeLib.Data.AppDbContext _context;

        public DetailsModel(RecipeLib.Data.AppDbContext context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);

            if (recipe is not null)
            {
                Recipe = recipe;

                return Page();
            }

            return NotFound();
        }
    }
}
