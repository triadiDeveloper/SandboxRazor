using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxRazor.Helper;
using SandboxRazor.Service.BC365;

namespace SandboxRazor.Pages.BC365.CustomerPages
{
    public class IndexModel : PageModel
    {
        private readonly CustomerService _customerService;

        public IndexModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; } = string.Empty;

        public PaginatedListHelper<Customer> Customers { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        private const int DefaultPageSize = 10;
        private const int ApiPageSize = 1000;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            try
            {
                var allCustomers = await _customerService.FetchCustomersAsync(ApiPageSize);

                // Apply search filtering if a query is provided
                var filteredCustomers = FilterCustomers(allCustomers, SearchQuery);

                // Paginate the filtered results
                PaginateCustomers(filteredCustomers, pageNumber);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error fetching customers: {ex.Message}");
                Customers = new PaginatedListHelper<Customer>(new List<Customer>(), 0, pageNumber, DefaultPageSize);
            }
        }

        /// <summary>
        /// Filters the customers based on the search query.
        /// </summary>
        private List<Customer> FilterCustomers(List<Customer> customers, string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return customers;
            }

            return customers.Where(c =>
                c.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.No.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                c.City.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Handles pagination logic for customers.
        /// </summary>
        private void PaginateCustomers(List<Customer> customers, int pageNumber)
        {
            var totalRecords = customers.Count;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)DefaultPageSize);
            CurrentPage = Math.Max(1, Math.Min(pageNumber, TotalPages)); // Ensure the current page is valid

            var pagedCustomers = customers
                .Skip((CurrentPage - 1) * DefaultPageSize)
                .Take(DefaultPageSize)
                .ToList();

            Customers = new PaginatedListHelper<Customer>(pagedCustomers, totalRecords, CurrentPage, DefaultPageSize);
        }
    }
}