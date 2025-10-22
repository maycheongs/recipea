using Recipea.Models;

namespace Recipea.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe
                    {
                        Title = "Avocado Toast",
                        Description = "Quick and healthy breakfast that's perfect for busy mornings. Creamy avocado on crispy toast with a sprinkle of salt and pepper.",
                        Ingredients = "2 slices bread\n1 ripe avocado\nSalt to taste\nFreshly ground black pepper\nRed pepper flakes (optional)\nLemon juice (optional)",
                        Instructions = "Toast the bread until golden brown\nMash the avocado in a bowl\nSpread mashed avocado evenly on toast\nSeason with salt and pepper to taste\nOptional: add red pepper flakes and a squeeze of lemon",
                        ImageUrl = "https://i.pinimg.com/1200x/56/e9/af/56e9afb4ddd1ec1e5c6531d229a8fbab.jpg",
                        Source = "Recipea Kitchen",
                        ActiveTime = "5 Minutes",
                        TotalTime = "5 Minutes",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Title = "Garlic Butter Shrimp",
                        Description = "Simple, flavorful dinner that comes together in minutes. Succulent shrimp cooked in aromatic garlic butter with a bright lemon finish.",
                        Ingredients = "1 lb large shrimp, peeled and deveined\n4 cloves garlic, minced\n3 tablespoons butter\n2 tablespoons olive oil\n1 lemon, juiced\nFresh parsley, chopped\nSalt and pepper to taste",
                        Instructions = "Heat butter and oil in a large skillet over medium-high heat\nAdd minced garlic and sauté for 30 seconds until fragrant\nAdd shrimp to the pan in a single layer\nCook for 2-3 minutes per side until pink and opaque\nSqueeze lemon juice over shrimp\nGarnish with fresh parsley and serve immediately",
                        ImageUrl = "https://i.pinimg.com/736x/0b/56/05/0b5605e5a895a109330d28dc972f8a98.jpg",
                        Source = "Quick Dinners",
                        ActiveTime = "10 Minutes",
                        TotalTime = "15 Minutes",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Recipe
                    {
                        Title = "Blueberry Pancakes",
                        Description = "Fluffy pancakes bursting with fresh blueberries. A weekend breakfast favorite that the whole family will love!",
                        Ingredients = "1 1/2 cups all-purpose flour\n2 tablespoons sugar\n2 teaspoons baking powder\n1/2 teaspoon salt\n1 1/4 cups milk\n1 egg\n3 tablespoons melted butter\n1 cup fresh blueberries",
                        Instructions = "In a large bowl, whisk together flour, sugar, baking powder, and salt\nIn another bowl, whisk together milk, egg, and melted butter\nPour wet ingredients into dry ingredients and mix until just combined (don't overmix)\nGently fold in blueberries\nHeat a griddle or skillet over medium heat and lightly grease\nPour 1/4 cup batter for each pancake\nCook until bubbles form on surface, then flip and cook until golden\nServe warm with maple syrup and extra blueberries",
                        ImageUrl = "https://i.pinimg.com/736x/5b/db/3b/5bdb3b819c003aad87382b6da7205e70.jpg",
                        Source = "Breakfast Classics",
                        ActiveTime = "15 Minutes",
                        TotalTime = "25 Minutes",
                        CreatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
