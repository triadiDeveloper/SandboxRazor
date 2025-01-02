﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SandboxRazor.Models.Pharmacy;

namespace SandboxRazor.Pages.Pharmacy.MedicationPages
{
    public class DetailsModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public DetailsModel(SandboxRazor.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public Medication Medication { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications.AsNoTracking()
                            .Include(m => m.Supplier)  // This eagerly loads the Supplier data
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (medication == null)
            {
                return NotFound();
            }
            else
            {
                Medication = medication;
            }
            return Page();
        }
    }
}
