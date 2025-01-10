using SandboxRazor.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SandboxRazor.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;
        private readonly SandboxRazor.Models.PersistenceDbContext _context;
        private readonly CurrentUserHelper _currentUserHelper;

        public DashboardModel(ILogger<DashboardModel> logger, Models.PersistenceDbContext context, CurrentUserHelper currentUserHelper)
        {
            _logger = logger;
            _context = context;
            _currentUserHelper = currentUserHelper;
        }

        public void OnGet()
        {
            var getUserName = _currentUserHelper.GetCurrentUserName();

            var applicationusers = _context.ApplicationUsers.AsNoTracking()
                .Where(x => x.UserName == getUserName)
                .Include(i => i.Employee)
                .Include(i => i.Companies)
                .Include(i => i.Infos)
                .Include(i => i.Roles)
                .Include(i => i.Tokens)
                .Include(i => i.Logins)
                .Include(i => i.Claims)
                .ToList();
        }
    }
}