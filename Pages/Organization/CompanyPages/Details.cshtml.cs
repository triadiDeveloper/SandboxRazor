using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxRazor.Models.Organization;
using SandboxRazor.Service;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class DetailsModel : PageModel
    {
        private readonly EntityService<Company> _entityService;

        public DetailsModel(EntityService<Company> entityService)
        {
            _entityService = entityService;
        }

        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _entityService.GetEntityByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                Company = company;
            }
            return Page();
        }
    }
}
