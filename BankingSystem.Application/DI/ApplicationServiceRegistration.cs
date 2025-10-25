using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankingSystem.Application.DI;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
