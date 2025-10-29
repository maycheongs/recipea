using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Services;

var builder = WebApplication.CreateBuilder(args);

// Toggle to simulate production mode locally
// Set ASPNETCORE_ENVIRONMENT=Production or uncomment the line below:
// builder.Environment.EnvironmentName = "Production";

// Add services
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// Add HttpClient and Spoonacular service
builder.Services.AddHttpClient<SpoonacularService>();

// Add AWS S3 service
builder.Services.AddSingleton<S3Service>();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await DbSeeder.SeedAsync(db, userManager);
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Custom 404 handler for production
if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            context.Request.Path = "/NotFound";
            context.Response.StatusCode = 404;
            await next();
        }
    });
}


await app.RunAsync();
