using SandboxRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace SandboxRazor.Extensions
{
    public class NavigationMiddleware
    {
        private readonly RequestDelegate _next;  // RequestDelegate is automatically injected

        public NavigationMiddleware(RequestDelegate next)  // ASP.NET Core will provide the next delegate automatically
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = context.RequestServices.CreateScope())  // Use context.RequestServices to create a scope
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PersistenceDbContext>();

                // Fetch navigation data from the database
                var navigations = await dbContext.Navigations.OrderBy(n => n.Index).ToListAsync();

                // Add navigations to HttpContext.Items so it's available globally in the request
                context.Items["Navigations"] = navigations;

                // Call the next middleware in the pipeline
                await _next(context);
            }
        }
    }
}