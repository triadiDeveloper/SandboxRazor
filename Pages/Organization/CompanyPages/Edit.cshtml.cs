using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Organization;
using SandboxRazor.Helper;

namespace SandboxRazor.Pages.Organization.CompanyPages
{
    public class EditModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;
        private readonly CurrentUserHelper _currentUserHelper;

        public EditModel(SandboxRazor.Models.PersistenceDbContext context, CurrentUserHelper currentUserHelper)
        {
            _context = context;
            _currentUserHelper = currentUserHelper;
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            Company = company;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var results = await _context.Companies.FirstOrDefaultAsync(m => m.Id == Company.Id);
            if (results == null)
            {
                return NotFound();
            }

            results.Code = Company.Code;
            results.Name = Company.Name;
            results.Note = Company.Note;
            results.ModifiedUser = _currentUserHelper.GetCurrentUserName();
            results.ModifiedDate = DateTime.Now;

            _context.Attach(results).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(Company.Id))
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

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
