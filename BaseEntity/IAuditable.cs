using SandboxRazor.Helper;

namespace SandboxRazor.BaseEntity;

public interface IAuditable
{
    string? CreatedUser { get; set; }
    DateTime? CreatedDate { get; set; }
    string? ModifiedUser { get; set; }
    DateTime? ModifiedDate { get; set; }

    void SetAuditCreatedFields(CurrentUserHelper currentUserHelper);
    void SetAuditModifiedFields(CurrentUserHelper currentUserHelper);
}
public interface INotAuditable
{

}
