using System.ComponentModel.DataAnnotations;

namespace RecipeLib.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        
        // Additional metadata
        public string? Source { get; set; }
        public string? ActiveTime { get; set; }
        public string? TotalTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
