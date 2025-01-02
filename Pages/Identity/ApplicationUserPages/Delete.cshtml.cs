﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Identity;

namespace SandboxRazor.Pages.Identity.ApplicationUserPages
{
    public class DeleteModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public DeleteModel(SandboxRazor.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationuser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (applicationuser == null)
            {
                return NotFound();
            }
            else
            {
                ApplicationUser = applicationuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationuser = await _context.ApplicationUsers.FindAsync(id);
            if (applicationuser != null)
            {
                ApplicationUser = applicationuser;
                _context.ApplicationUsers.Remove(ApplicationUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
