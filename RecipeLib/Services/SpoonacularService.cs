using RecipeLib.Models;
using System.Text.Json;

namespace RecipeLib.Services
{
    public class SpoonacularService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SpoonacularService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.spoonacular.com/");
            _apiKey = configuration["Spoonacular:ApiKey"] ?? throw new InvalidOperationException("Spoonacular API key not found");
        }

        public async Task<Recipe?> ImportRecipeFromUrl(string url)
        {
            try
            {
                // First, extract recipe from URL using Spoonacular's Extract Recipe endpoint
                var extractUrl = $"recipes/extract?url={Uri.EscapeDataString(url)}&apiKey={_apiKey}";
                
                var response = await _httpClient.GetAsync(extractUrl);
                
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var spoonacularRecipe = JsonSerializer.Deserialize<SpoonacularRecipeResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (spoonacularRecipe == null)
                {
                    return null;
                }

                // Convert Spoonacular recipe to our Recipe model
                var recipe = new Recipe
                {
                    Title = spoonacularRecipe.Title ?? "Imported Recipe",
                    Description = spoonacularRecipe.Summary != null 
                        ? StripHtmlTags(spoonacularRecipe.Summary) 
                        : "",
                    ImageUrl = spoonacularRecipe.Image ?? "",
                    Source = spoonacularRecipe.SourceName ?? spoonacularRecipe.SourceUrl ?? "",
                    ActiveTime = spoonacularRecipe.ReadyInMinutes.HasValue 
                        ? $"{spoonacularRecipe.ReadyInMinutes} Minutes" 
                        : null,
                    TotalTime = spoonacularRecipe.ReadyInMinutes.HasValue 
                        ? $"{spoonacularRecipe.ReadyInMinutes} Minutes" 
                        : null,
                    Ingredients = FormatIngredients(spoonacularRecipe.ExtendedIngredients),
                    Instructions = FormatInstructions(spoonacularRecipe.Instructions, spoonacularRecipe.AnalyzedInstructions),
                    CreatedAt = DateTime.UtcNow
                };

                return recipe;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string FormatIngredients(List<SpoonacularIngredient>? ingredients)
        {
            if (ingredients == null || ingredients.Count == 0)
            {
                return "";
            }

            return string.Join("\n", ingredients.Select(i => i.Original ?? i.Name ?? ""));
        }

        private string FormatInstructions(string? instructionsText, List<SpoonacularInstructionGroup>? analyzedInstructions)
        {
            // Try to use analyzed instructions first (more structured)
            if (analyzedInstructions != null && analyzedInstructions.Count > 0)
            {
                var steps = new List<string>();
                foreach (var group in analyzedInstructions)
                {
                    if (group.Steps != null)
                    {
                        foreach (var step in group.Steps)
                        {
                            if (!string.IsNullOrWhiteSpace(step.Step))
                            {
                                steps.Add(step.Step);
                            }
                        }
                    }
                }
                
                if (steps.Count > 0)
                {
                    return string.Join("\n", steps);
                }
            }

            // Fall back to plain instructions text
            if (!string.IsNullOrWhiteSpace(instructionsText))
            {
                return StripHtmlTags(instructionsText);
            }

            return "";
        }

        private string StripHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }

            // Simple HTML tag removal
            var text = System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
            // Decode HTML entities
            text = System.Net.WebUtility.HtmlDecode(text);
            return text.Trim();
        }
    }

    // Spoonacular API response models
    public class SpoonacularRecipeResponse
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Summary { get; set; }
        public string? SourceName { get; set; }
        public string? SourceUrl { get; set; }
        public int? ReadyInMinutes { get; set; }
        public int? Servings { get; set; }
        public List<SpoonacularIngredient>? ExtendedIngredients { get; set; }
        public string? Instructions { get; set; }
        public List<SpoonacularInstructionGroup>? AnalyzedInstructions { get; set; }
    }

    public class SpoonacularIngredient
    {
        public string? Name { get; set; }
        public string? Original { get; set; }
    }

    public class SpoonacularInstructionGroup
    {
        public string? Name { get; set; }
        public List<SpoonacularInstructionStep>? Steps { get; set; }
    }

    public class SpoonacularInstructionStep
    {
        public int Number { get; set; }
        public string? Step { get; set; }
    }
}

