using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandboxRazor.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace SandboxRazor.Pages.Identity.ApplicationUserPages
{
    public class CreateModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public CreateModel(SandboxRazor.Models.PersistenceDbContext context, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IActionResult OnGet()
        {
            ViewData["EmployeeList"] = new SelectList(
                     _context.Employees.Select(s => new
                     {
                         Id = s.Id,
                         DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                     }),
                     "Id",
                     "DisplayText"
                 );
            return Page();
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; } = default!;
        public List<ApplicationUserCompany> ApplicationUserCompanies { get; set; } = new List<ApplicationUserCompany>();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hash the password before saving
            if (ApplicationUser.PasswordOwn != null)
            {
                var hashedPassword = _passwordHasher.HashPassword(ApplicationUser, ApplicationUser.PasswordOwn);
                ApplicationUser.PasswordHash = hashedPassword;  // Save the hashed password instead of the plain one
                ApplicationUser.CreatedUser = "SYS";
            }


            _context.ApplicationUsers.Add(ApplicationUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
