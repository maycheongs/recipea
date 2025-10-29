using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Models;

namespace Recipea.Pages.Recipes
{
    [Authorize]
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

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);

            // Check if recipe exists and belongs to current user
            if (recipe is not null && recipe.UserId == currentUserId)
            {
                Recipe = recipe;
                return Page();
            }

            return NotFound();
        }
    }
}
