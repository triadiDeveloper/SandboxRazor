using SandboxRazor.BaseEntity;
using SandboxRazor.Models.Organization;

namespace SandboxRazor.Models.Identity
{
    public class UserApprovalDetail : BaseDomainDetail
    {
        public int UserApprovalId { get; set; }
        public virtual UserApproval? UserApproval { get; set; }
        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }

}
