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
                    },
                    new Recipe
                    {
                        Title = "Hainanese Chicken Rice",
                        Description = "The recipe Hainanese Chicken Rice can be made in around 1 hour. One serving contains 819 calories, 29g of protein, and 35g of fat. This recipe serves 4. This recipe from requires ginger ), ginger ), butter, and salt. Kerabu Rice (Rice Salad), Cauliflower, Brown Rice, and Vegetable Fried Rice, and Avocado & Feta Cheese Creamy Rice With Grilled Chicken are very similar to this recipe.",
                        Ingredients = "[CHICKEN]\n1 inch ginger ((peeled and smashed) (30g))\n3 chicken leg quarters ((thighs and drums attached))\nSalt (and pepper)\n½ cucumber ((sliced thinly into coins))\n4 Napa cabbage leaves ((sliced))\n[RICE]\n3 tbsp butter\n2 cloves garlic ((minced))\n1 inch ginger ((peeled and smashed) (30g))\n2 cups long grain rice ((rinsed and drained) (400g))\n2½ cups water (or chicken broth (600ml))\n½ tsp salt\n[CHILLI SAUCE]\n5 fresh red chilies\n1 inch ginger ((peeled))\n3 cloves garlic\n¼ cup rice vinegar ((60ml))\n2 tsp sugar\n½ tsp salt\n[SESAME GARLIC SAUCE]\n2 cloves garlic ((minced))\n2 tbsp sesame oil\n2 tbsp soy sauce",
                        Instructions = "[Chicken rice]\nBring a pot of water with ginger (enough to cover chicken by about an inch on the top) to boil. Rub chicken leg quarters with some salt and lower them into boiling water. Bring water back up to a boil. Reduce heat to medium and allow it to simmer for 5 minutes if using electric stove or 7 minutes if using gas stove. Turn off heat and leave chicken to poach in boiling water on the stove for 30 minutes. Do not open lid. **\nMelt butter in pot. Sauté garlic and ginger for 1 to 2 minutes. Add rice and fry for 2 to 3 minutes.\nAdd 2½ cups (600ml) water and salt to pot and bring it to a boil. Reduce heat to medium and allow it to simmer until all water is absorbed, about 10 minutes. Turn heat down to the lowest setting and leave pot on for another 5 minutes.\nTurn off heat, remove pot. Allow cooked rice to sit for 10 minutes before serving. Alternatively, transfer rice to rice cooker. Add water and salt and press the cook button.\n[Chilli sauce]\nBlend all garlic chili sauce ingredients until fine. Remove and divide into 4 small dishes. \nDrizzle a little soy sauce and a few drops of sesame oil onto the chili sauce. \nStore the remaining chili sauce in a glass jar.\n[Sesame Garlic Sauce]\nPlace garlic and sesame oil in a small microwaveable dish and microwave on high for 1½ minutes. Remove and mix in 2 tablespoon soy sauce. Set aside for now.\n[Serve]\nLine a dinner plate with sliced cucumbers. When chicken is ready, remove and immediately submerge it in a cold bath for 2 to 3 minutes. Reserve the chicken broth for soup. Remove bones from chicken leg quarters and cut into bite size pieces. Place onto sliced cucumbers. Drizzle the prepared sesame garlic sauce over chicken.\nSkim off fat from reserved chicken broth. Discard ginger. Bring it back up to a boil. Season with salt and pepper. Add Napa cabbage and allow it cook for 2 to 3 minutes. Turn off heat. Ladle into 4 serving bowls.\nTo serve, divide rice onto 4 plates. Dish a portion of chicken onto each plate. Serve immediately with garlic chili sauce and a bowl of soup for each place setting.",
                        ImageUrl = "https://www.rotinrice.com/wp-content/uploads/2011/09/HainaneseChickenRice-1.jpg",
                        Source = "Roti 'n' Rice",
                        ActiveTime = "20 Minutes",
                        TotalTime = "60 Minutes",
                        CreatedAt = DateTime.UtcNow
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
