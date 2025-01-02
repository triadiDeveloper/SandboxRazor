using Newtonsoft.Json;
using SandboxRazor.Helper;

namespace SandboxRazor.Service.BC365
{
    public class CustomerService
    {
        private readonly RestSharpHelper _restSharpHelper;

        public CustomerService(IConfiguration configuration, ILogger<RestSharpHelper> restSharpLogger)
        {
            _restSharpHelper = new RestSharpHelper(configuration, restSharpLogger);
        }

        public async Task<List<Customer>> FetchCustomersAsync(int pageSize = 1000)
        {
            try
            {
                return await _restSharpHelper.GetAsync<Customer>("Customers?", pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching customers: {ex.Message}", ex);
            }
        }
    }

    public class Customer
    {
        public string No { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
    }
}