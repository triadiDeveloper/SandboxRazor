namespace SandboxRazor.Helper
{
    public class CurrentUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserName()
        {
            // Get the logged-in user's name or return "SYS" if no user is logged in
            return _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "SYS";
        }
    }
}
