using Microsoft.EntityFrameworkCore;

namespace RecipeLib.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // We'll add DbSets later
    }
}
