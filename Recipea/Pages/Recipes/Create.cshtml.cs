using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recipea.Data;
using Recipea.Models;
using Recipea.Services;

namespace Recipea.Pages.Recipes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Recipea.Data.AppDbContext _context;
        private readonly SpoonacularService _spoonacularService;
        private readonly S3Service _s3Service;

        public CreateModel(Recipea.Data.AppDbContext context, SpoonacularService spoonacularService, S3Service s3Service)
        {
            _context = context;
            _spoonacularService = spoonacularService;
            _s3Service = s3Service;
        }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

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

            // Set the current user as the recipe owner
            Recipe.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

            // Handle image upload if a file was uploaded
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    // Upload to AWS S3
                    using var stream = ImageFile.OpenReadStream();
                    var imageUrl = await _s3Service.UploadImageAsync(stream, ImageFile.FileName);
                    
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        Recipe.ImageUrl = imageUrl;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(nameof(ImageFile), $"Upload failed: {ex.Message}");
                    return Page();
                }
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

                // Set the current user as the recipe owner
                recipe.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

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
