using Microsoft.EntityFrameworkCore;
using Recipea.Models; 

namespace Recipea.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
