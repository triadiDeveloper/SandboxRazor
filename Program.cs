using SandboxRazor.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Register CurrentUserHelper
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddLogging();

// Add services to the container
builder.Services.AddPersistenceServices();

// Add services to the container
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configure SQL Server Configuration
builder.Services.ConfigurePersistenceServices(builder.Configuration);

// Add authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/ApplicationUserLoginPages/Login";
        options.LogoutPath = "/Identity/ApplicationUserLoginPages/Logout";
        options.AccessDeniedPath = "/Identity/ApplicationUserLoginPages/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Expiry time
        options.SlidingExpiration = true;  // Enable sliding expiration
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Apply migrations
await PersistenceServiceRegistration.ApplyMigrations(app.Services);

// Seed Navigations Menu
await app.ConfigureSeedNavigations();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Use HSTS in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Register custom middlewares
app.UseCustomMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (Including Login page)
app.MapRazorPages();

// Ensure the login page is the fallback when no other route matches
app.MapFallbackToPage("/Identity/ApplicationUserLoginPages/Login");

app.Run();