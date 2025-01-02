using SandboxRazor.Helper;

namespace SandboxRazor.BaseEntity;

public interface IAuditable
{
    string? CreatedUser { get; set; }
    DateTime? CreatedDate { get; set; }
    string? ModifiedUser { get; set; }
    DateTime? ModifiedDate { get; set; }

    void SetAuditFields(CurrentUserHelper currentUserHelper);
}
public interface INotAuditable
{

}
