using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Organization;
using SandboxRazor.Helper;
using SandboxRazor.Service;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class EditModel : PageModel
    {
        private readonly EntityService<Company> _entityService;

        public EditModel(SandboxRazor.Models.PersistenceDbContext context, CurrentUserHelper currentUserHelper)
        {
            _entityService = new EntityService<Company>(currentUserHelper, context);
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

            Company = company;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingCompany = await _entityService.GetEntityByIdAsync(Company.Id);
            if (existingCompany == null)
            {
                return NotFound();
            }

            existingCompany.Code = Company.Code;
            existingCompany.Name = Company.Name;
            existingCompany.Note = Company.Note;

            try
            {
                await _entityService.UpdateEntityAsync(existingCompany);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _entityService.EntityExistsAsync(Company.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}