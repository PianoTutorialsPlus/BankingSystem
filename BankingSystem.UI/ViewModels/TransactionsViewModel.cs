using BankingSystem.Application.DTOs;
using BankingSystem.UI.Models;
using BankingSystem.UI.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels;

public class TransactionsViewModel : INotifyPropertyChanged
{
    private readonly IApiService _api;
    public ObservableCollection<TransactionDto> Transactions { get; } = new();

    public ICommand LoadCommand { get; }
    public ICommand AddTransactionCommand { get; }

    public TransactionsViewModel(IApiService api)
    {
        _api = api;
        LoadCommand = new RelayCommand(async _ => await LoadAsync());
        AddTransactionCommand = new RelayCommand(async _ => await AddTransactionAsync());
    }

    public async Task LoadAsync(int accountId = 1)
    {
        Transactions.Clear();
        var list = await _api.GetTransactionsByAccountAsync(accountId);
        foreach (var t in list) Transactions.Add(t);
    }

    public async Task AddTransactionAsync()
    {
        var req = new PerformTransactionRequest(DateTime.UtcNow, 10m, "Test", "", Guid.NewGuid().ToString(), 1);
        await _api.PerformTransactionAsync(1, req);
        await LoadAsync(1);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}
