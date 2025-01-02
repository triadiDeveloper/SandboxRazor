﻿using Microsoft.AspNetCore.Identity;

namespace SandboxRazor.Models.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
