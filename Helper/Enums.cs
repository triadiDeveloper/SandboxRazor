using System.ComponentModel.DataAnnotations;

namespace SandboxRazor.Helper
{
    public enum EnumItemCategory : short
    {
        [Display(Name = "Alat Kesehatan")]
        MedicalDevices = 0,
        [Display(Name = "Obat")]
        Medicine = 1
    }
}
