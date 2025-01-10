using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxRazor.Models.Organization;
using SandboxRazor.Service;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class DeleteModel : PageModel
    {
        private readonly EntityService<Company> _entityService;

        public DeleteModel(EntityService<Company> entityService)
        {
            _entityService = entityService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _entityService.DeleteEntityAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
