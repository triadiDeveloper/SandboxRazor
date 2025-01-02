using SandboxRazor.Models;
using SandboxRazor.Helper;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Extensions;

public static class NavigationExtensions
{
    // This method seeds the navigation data into the database
    public static async Task ConfigureSeedNavigations(this WebApplication app)
    {
        // Add your NavigationMiddleware to the pipeline
        app.UseMiddleware<NavigationMiddleware>();

        // Create a scope to get the DbContext
        using (var scope = app.Services.CreateScope()) // Create a service scope here
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PersistenceDbContext>();

            // Fetch all current navigations in the database
            var existingNavigations = await dbContext.Navigations.ToListAsync();

            // Get the new list of navigations from the NavigationHelper
            var newNavigations = NavigationHelper.DataInitializationSeedNavigations();

            // Add new navigations (those that don't exist in the DB)
            var navigationsToAdd = newNavigations.Where(n => !existingNavigations.Any(e => e.Code == n.Code)).ToList();
            if (navigationsToAdd.Any())
            {
                dbContext.Navigations.AddRange(navigationsToAdd);
            }

            // Update existing navigations (those that are found in both the DB and the new list, but have differences)
            var navigationsToUpdate = existingNavigations.Where(e => newNavigations.Any(n => n.Code == e.Code && (n.Name != e.Name || n.Icon != e.Icon || n.Url != e.Url))).ToList();
            foreach (var existingNavigation in navigationsToUpdate)
            {
                var updatedNavigation = newNavigations.First(n => n.Code == existingNavigation.Code);
                existingNavigation.Name = updatedNavigation.Name;
                existingNavigation.Icon = updatedNavigation.Icon;
                existingNavigation.Url = updatedNavigation.Url;
            }

            // Delete navigations that are in the DB but not in the new list
            var navigationsToDelete = existingNavigations.Where(e => !newNavigations.Any(n => n.Code == e.Code)).ToList();
            if (navigationsToDelete.Any())
            {
                dbContext.Navigations.RemoveRange(navigationsToDelete);
            }

            // Save the changes to the database (add, update, delete)
            await dbContext.SaveChangesAsync();
        }
    }
}