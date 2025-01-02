﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Pharmacy;

namespace SandboxRazor.Pages.Pharmacy.TransactionPages
{
    public class DeleteModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public DeleteModel(SandboxRazor.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                Transaction = transaction;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                Transaction = transaction;
                _context.Transactions.Remove(Transaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
