using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Pharmacy;
using SandboxRazor.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SandboxRazor.Pages.Pharmacy.SupplierPages
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

        public PaginatedListHelper<Supplier> Supplier { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var suppliers = _context.Suppliers.AsQueryable();

            // Filter based on search query if provided
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                suppliers = suppliers.Where(c => c.Name.Contains(SearchQuery) || c.Code.Contains(SearchQuery));
            }

            var pageSize = 10;
            TotalPages = (int)Math.Ceiling(await suppliers.CountAsync() / (double)pageSize);
            CurrentPage = pageNumber;

            Supplier = await PaginatedListHelper<Supplier>.CreateAsync(suppliers, pageNumber, pageSize);
        }
    }
}