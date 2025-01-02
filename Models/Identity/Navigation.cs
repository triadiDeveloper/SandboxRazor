namespace SandboxRazor.Models.Identity
{
    public class Navigation: BaseEntity.BaseCodeName
    {
        public string? ParentCode { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public int Index { get; set; } // Urutan Navigation
    }
}
