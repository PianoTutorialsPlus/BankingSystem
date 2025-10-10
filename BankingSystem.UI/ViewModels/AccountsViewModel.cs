using BankingSystem.UI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BankingSystem.Application.DTOs;

namespace BankingSystem.UI.ViewModels;

public class AccountsViewModel : INotifyPropertyChanged
{
    private readonly IApiService _api;
    public ObservableCollection<AccountDto> Accounts { get; } = new();

    public ICommand LoadCommand { get; }
    public ICommand CreateAccountCommand { get; }

    public AccountsViewModel(IApiService api)
    {
        _api = api;
        LoadCommand = new RelayCommand(async _ => await LoadAsync());
        CreateAccountCommand = new RelayCommand(async _ => await CreateAsync());
    }

    public async Task LoadAsync()
    {
        Accounts.Clear();
        var list = await _api.GetAccountsAsync();
        foreach (var a in list) Accounts.Add(a);
    }

    public async Task CreateAsync()
    {
        // simple create: ask for customer id via simple dialog or fixed id for demo
        var customerId = 1;
        var open = 0m;
        await _api.CreateAccountAsync(new CreateAccountRequest(customerId, open));
        await LoadAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}
