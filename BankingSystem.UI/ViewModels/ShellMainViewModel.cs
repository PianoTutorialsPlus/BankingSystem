using BankingSystem.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels;

public class ShellMainViewModel : INotifyPropertyChanged
{
    private readonly INavigationService _nav;

    public ShellMainViewModel(INavigationService nav)
    {
        _nav = nav;
        NavigateCustomersCommand = new RelayCommand(_ => Navigate("Customers"));
        NavigateAccountsCommand = new RelayCommand(_ => Navigate("Accounts"));
        NavigateTransactionsCommand = new RelayCommand(_ => Navigate("Transactions"));

        // start view:
        CurrentView = _nav.GetViewFor("Customers");
    }

    private object? _currentView;
    public object? CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }

    public ICommand NavigateCustomersCommand { get; }
    public ICommand NavigateAccountsCommand { get; }
    public ICommand NavigateTransactionsCommand { get; }

    private void Navigate(string key)
    {
        var view = _nav.GetViewFor(key);
        if (view != null) CurrentView = view;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

