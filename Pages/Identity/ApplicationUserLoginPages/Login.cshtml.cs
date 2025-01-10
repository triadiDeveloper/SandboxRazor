using SandboxRazor.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SandboxRazor.Pages.Identity.ApplicationUserLoginPages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly SandboxRazor.Models.PersistenceDbContext _context;

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public bool LoginFailed { get; set; }

        public LoginModel(SandboxRazor.Models.PersistenceDbContext context, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void OnGet()
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, proxy-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attempt to find the user by the username
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName == ApplicationUser.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            // Check password (Assuming PasswordOwn is the raw password input)
            var passwordValid = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, ApplicationUser.PasswordOwn);
            if (passwordValid == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                LoginFailed = true; // Set to true if validation fails
                return Page();
            }

            // Create the claims
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Convert user.Id (Guid) to string
                };

            // Create the identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create the principal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToPage("/Dashboard"); // Redirect to home page or dashboard after login
        }
    }
}