using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Navigation;
using BankingSystem.UI.Services;
using BankingSystem.UI.Services.Account;
using BankingSystem.UI.Services.Base;
using BankingSystem.UI.Services.Customer;
using BankingSystem.UI.ViewModels;
using BankingSystem.UI.Views;
using System.Reflection;

namespace BankingSystem.UI.DI;

public class ViewModelLocator
{
    public static IContainer Container { get; private set; }

    static ViewModelLocator()
    {
        var builder = new ContainerBuilder();

        builder.RegisterAutoMapper(Assembly.GetExecutingAssembly());

        // Services
        builder.RegisterType<CustomerService>().As<ICustomerService>().SingleInstance();
        builder.RegisterType<AccountService>().As<IAccountService>().SingleInstance();

        builder.RegisterType<Client>().As<IClient>().SingleInstance();
        builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
        builder.RegisterType<CustomerVM>();

        // Views and ViewModels
        builder.RegisterType<ShellMainView>().AsSelf().SingleInstance();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(type => !type.IsAbstract && type.Name.EndsWith("ViewModel"))
            .PropertiesAutowired()
            .AsSelf()
            .InstancePerDependency();

        // RelayCommand
        builder.RegisterType<RelayCommand>().AsSelf();

        Container = builder.Build();
    }

    public ShellMainViewModel ShellMainViewModel => Container.Resolve<ShellMainViewModel>();
}
