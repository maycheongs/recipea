using Microsoft.EntityFrameworkCore;
using RecipeLib.Models; 

namespace RecipeLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
