using Autofac;
using BankingSystem.UI.Navigation;
using BankingSystem.UI.Services;
using BankingSystem.UI.ViewModels;
using BankingSystem.UI.Views;
using System.Windows;

namespace BankingSystem.UI;

public partial class App : System.Windows.Application
{
    private IContainer? _container;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var builder = new ContainerBuilder();

        // Services
        builder.RegisterType<ApiService>().As<IApiService>().SingleInstance();
        builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

        // Views and ViewModels
        builder.RegisterType<ShellMainView>().AsSelf().SingleInstance();
        builder.RegisterType<ShellMainViewModel>().AsSelf().SingleInstance();

        builder.RegisterType<CustomersView>().AsSelf();
        builder.RegisterType<CustomersViewModel>().AsSelf();

        builder.RegisterType<AccountsView>().AsSelf();
        builder.RegisterType<AccountsViewModel>().AsSelf();

        builder.RegisterType<TransactionsView>().AsSelf();
        builder.RegisterType<TransactionsViewModel>().AsSelf();

        // RelayCommand
        builder.RegisterType<RelayCommand>().AsSelf();

        _container = builder.Build();

        // Register navigation view factories
        var nav = _container.Resolve<INavigationService>();
        nav.Register("Customers", () => _container.Resolve<CustomersView>());
        nav.Register("Accounts", () => _container.Resolve<AccountsView>());
        nav.Register("Transactions", () => _container.Resolve<TransactionsView>());

        var shell = _container.Resolve<ShellMainView>();
        shell.Show();
    }
}

