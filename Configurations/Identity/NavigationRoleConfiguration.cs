using SandboxRazor.Models.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SandboxRazor.Configurations.Identity
{
    public class NavigationRoleConfiguration : IEntityTypeConfiguration<NavigationRole>
    {
        public void Configure(EntityTypeBuilder<NavigationRole> builder)
        {
            builder.ToTable("NavigationRole", schema: "Identity");
           
            builder.Property(x => x.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
            builder.Property(x => x.Version).IsRowVersion();
        }
    }
}
