using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.HumanResource;
using SandboxRazor.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SandboxRazor.Pages.HumanResource.EmployeePages
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

        public PaginatedListHelper<Employee> Employee { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var employees = _context.Employees.AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                employees = employees.Where(c => c.Name.Contains(SearchQuery) || c.Code.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await employees.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Employee = await PaginatedListHelper<Employee>.CreateAsync(employees, pageNumber, pageSize);
        }
    }
}