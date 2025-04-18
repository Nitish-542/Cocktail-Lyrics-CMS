using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICocktailService, CocktailService>();
builder.Services.AddScoped<IBartenderService, BartenderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ICocktailSongPairingService, CocktailSongPairingService>();





// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC + Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer(); // ?? Add this
builder.Services.AddSwaggerGen();           // ?? And this

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Enable Swagger in development
    app.UseSwagger();                         // ?? Enable Swagger middleware
    app.UseSwaggerUI(options =>               // ?? Swagger UI setup
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music & Mixology API V1");
        options.RoutePrefix = "swagger"; // browse at /swagger
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
