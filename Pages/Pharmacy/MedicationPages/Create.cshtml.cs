using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandboxRazor.Models.Pharmacy;
using SandboxRazor.Helper;

namespace SandboxRazor.Pages.Pharmacy.MedicationPages
{
    public class CreateModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        public CreateModel(SandboxRazor.Models.PersistenceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SupplierList"] = new SelectList(
                     _context.Suppliers.Select(s => new
                     {
                         Id = s.Id,
                         DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                     }),
                     "Id",
                     "DisplayText"
                 );

            var categoryList = new SelectList(EnumList.GetEnumSelectList<EnumItemCategory>(), "Value", "Text");

            ViewData["CategoryList"] = new SelectList(categoryList, "Value", "Text");

            return Page();
        }

        [BindProperty]
        public Medication Medication { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Medications.Add(Medication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
