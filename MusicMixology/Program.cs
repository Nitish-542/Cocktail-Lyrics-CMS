using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection for services
// Registering application services for Dependency Injection (DI).
builder.Services.AddScoped<ICocktailService, CocktailService>();
builder.Services.AddScoped<IBartenderService, BartenderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ICocktailSongPairingService, CocktailSongPairingService>();

// Database Connection
// Configuring the application's database connection string.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Enabling detailed database error pages in development mode.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity with roles + default UI
// Adding Identity services with roles support and the default UI for user authentication.
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // Enables role support
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC, Razor Pages, Swagger
// Registering services needed for Razor Pages, MVC, and Swagger for API documentation.
builder.Services.AddRazorPages(); // Needed for login/register UI
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations + seed roles/admin
// Ensuring database is migrated and seed roles (admin) are created.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Apply pending migrations

    // Seed roles and default admin user
    await RoleSeeder.SeedRolesAndAdminAsync(services);
}

// Middleware setup
// Configuring middleware pipeline based on environment (development vs. production).
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Provides migration endpoints in development
    app.UseSwagger(); // Enable Swagger UI for API documentation
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music & Mixology API V1");
        options.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
    });
}
else
{
    // Production setup
    app.UseExceptionHandler("/Home/Error"); // Generic error handler in production
    app.UseHsts(); // Enforces HTTPS
}

// Common middleware setup
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serves static files (e.g., images, CSS)

app.UseRouting(); // Enables routing

// Authentication & Authorization middleware
app.UseAuthentication(); // Required for login functionality
app.UseAuthorization(); // Required for access control

// Route mapping
// Configuring default route mapping for controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Enables Razor Pages (e.g., login, register, etc.)

app.Run(); // Starts the application
