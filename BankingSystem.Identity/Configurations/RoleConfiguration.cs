using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingSystem.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "2a95a9cd-c232-443a-a6d2-c613be45185b", // GUID Value
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "f701d759-adf9-47cd-8f22-5b21e9c52ac9",
                Name = "Administrator",
                NormalizedName = "ADMIN"
            });
    }
}
