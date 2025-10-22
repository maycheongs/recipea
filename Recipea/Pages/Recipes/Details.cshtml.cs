using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Models;

namespace Recipea.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly Recipea.Data.AppDbContext _context;

        public DetailsModel(Recipea.Data.AppDbContext context)
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
