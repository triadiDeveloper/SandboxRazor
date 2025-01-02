using SandboxRazor.Extensions;
using SandboxRazor.Helper;
using SandboxRazor.Models;
using SandboxRazor.Models.Identity;
using SandboxRazor.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SandboxRazor.Service.BC365;

var builder = WebApplication.CreateBuilder(args);

// Register CurrentUserHelper
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<CurrentUserHelper>();

builder.Services.AddLogging(); // Ensures loggers for all types are available
builder.Services.AddSingleton<RestSharpHelper>();
builder.Services.AddSingleton<CustomerService>();



// Add other services or dependencies you need
builder.Services.AddScoped<IEntityServiceFactory, EntityServiceFactory>(); 

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configure SQL Server Configuration
builder.Services.ConfigurePersistenceServices(builder.Configuration);

// Add authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/ApplicationUserLoginPages/Login";
        options.LogoutPath = "/LandingPages"; // Set logout URL
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

builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

var app = builder.Build();

// Apply migrations
await PersistenceServiceRegistration.ApplyMigrations(app.Services);

// Register the custom middleware
app.UseMiddleware<NavigationMiddleware>();

// Seed Navigations Menu
await app.ConfigureSeedNavigations();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (Including Login page)
app.MapRazorPages();

// Ensure the login page is the fallback when no other route matches
// Only do this if you really want a fallback to the Login page
// Otherwise, remove this line if it's unnecessary
app.MapFallbackToPage("/LandingPages");

app.Run();