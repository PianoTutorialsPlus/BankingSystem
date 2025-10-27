using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingSystem.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                // user
                RoleId = "2a95a9cd-c232-443a-a6d2-c613be45185b",
                UserId = "a2634141-eb89-4438-a70e-ad8f3ecbfe9b"
            },
            new IdentityUserRole<string>
            {
                // admin
                RoleId = "f701d759-adf9-47cd-8f22-5b21e9c52ac9",
                UserId = "918043f8-d092-4b9d-be3e-63eae8307e2b"
            });
    }
}
