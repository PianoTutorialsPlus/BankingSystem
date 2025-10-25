using BankingSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Persistence.DI;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankingDbContext>(opts
            => opts.UseSqlite(configuration.GetConnectionString("HrDatabaseConnectionString")));

        return services;
    }
}
