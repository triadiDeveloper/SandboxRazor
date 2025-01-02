using SandboxRazor.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandboxRazor.Models.Identity
{
    public class NavigationRole : BaseDomainDeep
    {
        [ForeignKey("Navigation")]
        public int NavigationId { get; set; }
        public virtual Navigation? Navigation { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}
