using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeLib.Data;
using RecipeLib.Models;
using RecipeLib.Services;

namespace RecipeLib.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly RecipeLib.Data.AppDbContext _context;
        private readonly SpoonacularService _spoonacularService;

        public CreateModel(RecipeLib.Data.AppDbContext context, SpoonacularService spoonacularService)
        {
            _context = context;
            _spoonacularService = spoonacularService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        // API endpoint for importing recipe from URL
        public async Task<IActionResult> OnPostImportFromUrlAsync([FromBody] ImportUrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Url))
            {
                return new JsonResult(new { success = false, error = "URL is required" }) { StatusCode = 400 };
            }

            try
            {
                var recipe = await _spoonacularService.ImportRecipeFromUrl(request.Url);

                if (recipe == null)
                {
                    return new JsonResult(new { success = false, error = "Could not import recipe from URL" }) { StatusCode = 400 };
                }

                // Save the imported recipe to the database
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                return new JsonResult(new { success = true, id = recipe.Id });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message }) { StatusCode = 500 };
            }
        }
    }

    public class ImportUrlRequest
    {
        public string Url { get; set; } = "";
    }
}
