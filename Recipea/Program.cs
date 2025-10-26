using Microsoft.EntityFrameworkCore;
using Recipea.Data;
using Recipea.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add HttpClient and Spoonacular service
builder.Services.AddHttpClient<SpoonacularService>();

// Add AWS S3 service
builder.Services.AddSingleton<S3Service>();

// Mock authentication setup (placeholder)
builder.Services.AddAuthentication("FakeCookieAuth")
    .AddCookie("FakeCookieAuth", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(db);
}

app.MapRazorPages();

app.Run();
