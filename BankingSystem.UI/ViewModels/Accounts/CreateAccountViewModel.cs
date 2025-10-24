using BankingSystem.UI.Models.Account;
using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Account;
using BankingSystem.UI.Services.Customer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels.Accounts;

public class CreateAccountViewModel 
{
    private readonly IAccountService accountService;
    private readonly ICustomerService customerService;
    public ObservableCollection<CustomerVM> Customers { get; } = new();
    public AccountVM Account { get; set; } = new();
    public ICommand SaveCommand { get; }
    public CreateAccountViewModel(IAccountService accountService, ICustomerService customerService)
    {
        this.accountService = accountService;
        this.customerService = customerService;
        SaveCommand = new RelayCommand(async _ => await SaveAsync());
        _ = LoadCustomersAsync();
    }

    private async Task SaveAsync()
    {
        var response = await accountService.CreateAccount(Account);

        if (response.Success)
            CloseWindow();

        if (response.ValidationErrors != null)
            foreach (var error in response.ValidationErrors)
                Account.SetErrors(error.Key, error.Value);
    }
    private async Task LoadCustomersAsync()
    {
        var customers = await customerService.GetCustomers();
        Customers.Clear();
        foreach (var c in customers)
            Customers.Add(c);
    }
    private void CloseWindow()
    {
        System.Windows.Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.DataContext == this)?
            .Close();
    }
}
