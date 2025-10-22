using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Models;
using System.Text.RegularExpressions;

namespace Recipea.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipes { get; set; } = new List<Recipe>();
        public string? SearchQuery { get; set; }
        public int? MaxActiveTime { get; set; }
        public int? MaxTotalTime { get; set; }

        public async Task OnGetAsync(string? searchQuery, int? maxActiveTime, int? maxTotalTime)
        {
            SearchQuery = searchQuery;
            MaxActiveTime = maxActiveTime;
            MaxTotalTime = maxTotalTime;

            var query = _context.Recipes.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(r => r.Title.Contains(searchQuery) || 
                                        r.Description.Contains(searchQuery) ||
                                        r.Ingredients.Contains(searchQuery));
            }

            Recipes = await query.ToListAsync();

            // Apply time filters (client-side for now since times are stored as strings)
            if (maxActiveTime.HasValue)
            {
                Recipes = Recipes.Where(r => ParseMinutes(r.ActiveTime) <= maxActiveTime.Value).ToList();
            }

            if (maxTotalTime.HasValue)
            {
                Recipes = Recipes.Where(r => ParseMinutes(r.TotalTime) <= maxTotalTime.Value).ToList();
            }
        }

        private int ParseMinutes(string? timeString)
        {
            if (string.IsNullOrEmpty(timeString))
                return 0;

            // Extract number from strings like "5 Minutes", "1 Hour", "1.5 Hours", etc.
            var match = Regex.Match(timeString, @"([\d.]+)\s*(minute|min|hour|hr)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var value = double.Parse(match.Groups[1].Value);
                var unit = match.Groups[2].Value.ToLower();
                
                if (unit.StartsWith("hour") || unit.StartsWith("hr"))
                {
                    return (int)(value * 60);
                }
                return (int)value;
            }
            return 0;
        }
    }
}
