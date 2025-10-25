using Autofac;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Infrastructure.Repositories;
using BankingSystem.Persistence.Repositories;


namespace BankingSystem.Persistence.DI;

public class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

        builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().SingleInstance();
        builder.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
    }
}
