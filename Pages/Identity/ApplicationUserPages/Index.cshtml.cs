using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Identity;
using SandboxRazor.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SandboxRazor.Pages.Identity.ApplicationUserPages
{
    public class IndexModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public IndexModel(SandboxRazor.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public PaginatedListHelper<ApplicationUser> ApplicationUser { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var applicationusers = _context.ApplicationUsers
                .Include(a => a.Employee).AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                applicationusers = applicationusers.Where(c => c.UserName.Contains(SearchQuery) || c.Email.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await applicationusers.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            ApplicationUser = await PaginatedListHelper<ApplicationUser>.CreateAsync(applicationusers, pageNumber, pageSize);
        }
    }
}
