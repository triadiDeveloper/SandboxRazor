namespace SandboxRazor.BaseEntity;

public interface ISoftDeletable
{
    bool? IsDeleted { get; set; }
}

