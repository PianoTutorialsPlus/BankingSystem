using BankingSystem.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Identity.DbContext;

public class BankingSystemDbContext : IdentityDbContext<ApplicationUser>
{
    public BankingSystemDbContext(DbContextOptions<BankingSystemDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(BankingSystemDbContext).Assembly);
    }
}
