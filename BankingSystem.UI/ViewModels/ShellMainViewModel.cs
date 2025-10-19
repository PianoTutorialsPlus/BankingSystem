using BankingSystem.UI.Navigation;
using BankingSystem.UI.ViewModels.Customers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels;

public class ShellMainViewModel : INotifyPropertyChanged
{
    private readonly INavigationService navigationService;

    public ShellMainViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
        NavigateCustomersCommand = new RelayCommand(_ => NavigateTo<CustomersViewModel>());
        NavigateAccountsCommand = new RelayCommand(_ => NavigateTo<AccountsViewModel>());
        NavigateTransactionsCommand = new RelayCommand(_ => NavigateTo<TransactionsViewModel>());

        // start view:
        CurrentViewModel = this.navigationService.Get<CustomersViewModel>();
    }

    private object? _currentView;
    public object? CurrentViewModel { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

    private void NavigateTo<TViewModel>() where TViewModel : class
    {
        var view = navigationService.Get<TViewModel>();
        if (view != null) CurrentViewModel = view;
    }

    public ICommand NavigateCustomersCommand { get; }
    public ICommand NavigateAccountsCommand { get; }
    public ICommand NavigateTransactionsCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

