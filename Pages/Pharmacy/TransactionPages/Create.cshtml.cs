using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SandboxRazor.Models.Pharmacy;
using Microsoft.AspNetCore.Mvc.Rendering;
using SandboxRazor.Helper;

namespace SandboxRazor.Pages.Pharmacy.TransactionPages
{
    public class CreateModel : PageModel
    {
        private readonly SandboxRazor.Models.PersistenceDbContext _context;
        private readonly CurrentUserHelper _currentUserHelper;

        public CreateModel(SandboxRazor.Models.PersistenceDbContext context, CurrentUserHelper currentUserHelper)
        {
            _context = context;
            _currentUserHelper = currentUserHelper;
        }

        [BindProperty]
        public Transaction Transaction { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();

        public void OnGet()
        {
            // Initialize Transaction if needed
            Transaction = new Transaction();
            Transaction.DocumentDate = DateTime.Now;

            // Prepare the dropdown list of medications
            ViewData["MedicationList"] = new SelectList(
                    _context.Medications.Select(s => new
                    {
                        Id = s.Id,
                        DisplayText = $"{s.Code} - {s.Name}"  // Concatenate Code and Name
                    }),
                    "Id",
                    "DisplayText"
                ).ToList() ;
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(DateTime hiddenDocumentDate)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var documentDate = hiddenDocumentDate;
            Console.WriteLine("Document Date: " + documentDate);  // You can log it if you want

            Transaction.CreatedUser = _currentUserHelper.GetCurrentUserName();

            // Generate the document number
            Transaction.DocumentNumber = await DocumentNumberHelper.GenerateDocumentNumberAsync(_context);

            //// Add the new transaction to the database
            //_context.Transactions.Add(Transaction);
            //await _context.SaveChangesAsync();

            //// Now handle the transaction details and update medication quantities
            //foreach (var detail in Transaction.TransactionDetails)
            //{
            //    var medication = await _context.Medications
            //        .FirstOrDefaultAsync(m => m.Id == detail.MedicationId);

            //    if (medication == null)
            //    {
            //        ModelState.AddModelError("", "Selected medication not found.");
            //        return Page();
            //    }

            //    // Check if there's enough stock to complete the transaction
            //    if (medication.Qty < detail.Qty)
            //    {
            //        ModelState.AddModelError("", $"Not enough stock for {medication.Name}. Available stock is {medication.Qty}.");
            //        return Page();
            //    }

            //    // Reduce the medication Qty
            //    medication.Qty -= detail.Qty;

            //    // Update the medication in the database
            //    _context.Medications.Update(medication);

            //    // Add the transaction detail
            //    detail.TransactionId = Transaction.Id; // Ensure the TransactionId is set
            //    _context.TransactionDetails.Add(detail);
            //}

            //// Save all changes to the database (both transaction and medications)
            //await _context.SaveChangesAsync();

            // Redirect back to the index page
            return RedirectToPage("./Index");
        }
    }
}