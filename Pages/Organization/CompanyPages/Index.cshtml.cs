using SandboxRazor.Helper;
using SandboxRazor.Models.Organization;
using SandboxRazor.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class IndexModel : PageModel
    {
        private readonly EntityService<Company> _entityService;

        public IndexModel(EntityService<Company> entityService)
        {
            _entityService = entityService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public PaginatedListHelper<Company> Company { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var pageSize = 10;

            // Use the EntityService to get filtered and paginated data
            Company = await _entityService.GetPaginatedAndFilteredAsync(
                query =>
                {
                    if (!string.IsNullOrEmpty(SearchQuery))
                    {
                        query = query.Where(c => c.Name.Contains(SearchQuery) || c.Code.Contains(SearchQuery));
                    }
                    return query;
                },
                pageNumber,
                pageSize
            );

            // Set pagination details
            CurrentPage = pageNumber;
            TotalPages = Company.TotalPages;
        }
    }
}