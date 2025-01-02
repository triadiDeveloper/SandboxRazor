using System.ComponentModel.DataAnnotations;

namespace SandboxRazor.BaseEntity;

public interface INote
{
    [StringLength(2000)]
    public string? Note { get; set; }
}

