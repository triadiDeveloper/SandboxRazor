using Microsoft.AspNetCore.Mvc;
using SandboxRazor.Models.Organization;
using SandboxRazor.Helper;
using SandboxRazor.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class CreateModel : PageModel
    {
        private readonly EntityService<Company> _entityService;

        public MessageHelper MessageHelper { get; set; } // MessageHelper for status messages

        // Initialize the services via Dependency Injection
        public CreateModel(SandboxRazor.Models.PersistenceDbContext context, CurrentUserHelper currentUserHelper)
        {
            MessageHelper = new MessageHelper(); // Ensure MessageHelper is initialized
            _entityService = new EntityService<Company>(currentUserHelper, context);
        }

        [BindProperty]
        public Company Company { get; set; } = default!; // Ensures Company is not null

        public IActionResult OnGet()
        {
            // Add your logic here for OnGet
            return Page();
        }

        // OnPostAsync method for handling form submissions
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the form is valid
            if (!ModelState.IsValid)
            {
                // Set error message if the form validation fails
                MessageHelper.SetErrorMessage("There were errors in the form.");
                return Page();
            }

            // Check if the company code already exists (using the generic duplicate checker)
            var isCodeDuplicate = await _entityService.IsDuplicateAsync("Code", Company.Code);

            if (isCodeDuplicate)
            {
                // If the code exists, show an error message
                MessageHelper.SetErrorMessage("A company with this code already exists.");
                return Page();
            }

            try
            {
                await _entityService.CreateEntityAsync(Company);
                MessageHelper.SetSuccessMessage("Company created successfully!");
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                MessageHelper.SetErrorMessage($"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}