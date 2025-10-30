using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Models;
using Recipea.Services;

namespace Recipea.Pages.Recipes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Recipea.Data.AppDbContext _context;
        private readonly S3Service _s3Service;
        private readonly IConfiguration _configuration;

        public EditModel(Recipea.Data.AppDbContext context, S3Service s3Service, IConfiguration configuration)
        {
            _context = context;
            _s3Service = s3Service;
            _configuration = configuration;
        }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public bool IsS3Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";

            var recipe =  await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            
            // Check if recipe exists and belongs to current user
            if (recipe == null || recipe.UserId != currentUserId)
            {
                return NotFound();
            }
            Recipe = recipe;
            
            // Check if the image URL is from S3
            IsS3Image = !string.IsNullOrEmpty(Recipe.ImageUrl) && _s3Service.IsS3Url(Recipe.ImageUrl);
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "";
            
            // Verify the recipe belongs to the current user
            var existingRecipe = await _context.Recipes.FindAsync(Recipe.Id);
            if (existingRecipe == null || existingRecipe.UserId != currentUserId)
            {
                return NotFound();
            }

            // Ensure the user ID remains the same
            Recipe.UserId = existingRecipe.UserId;

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

            // Update the existing recipe with the new values
            _context.Entry(existingRecipe).CurrentValues.SetValues(Recipe);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving: {ex.Message}");
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", $"Details: {ex.InnerException.Message}");
                }
                return Page();
            }

            return RedirectToPage("./Details", new { id = Recipe.Id });
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
