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

public class CreateAccountViewModel : INotifyPropertyChanged
{
    private readonly IAccountService accountService;
    private readonly ICustomerService customerService;
    public ObservableCollection<CustomerVM> Customers { get; } = new();
    public AccountVM Account { get; set; } = new();
    public CreateAccountViewModel(IAccountService accountService, ICustomerService customerService)
    {
        this.accountService = accountService;
        this.customerService = customerService;
        SaveCommand = new RelayCommand(async _ => await SaveAsync());
        _ = LoadCustomersAsync();
    }

    public ICommand SaveCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private async Task SaveAsync()
    {
        //if (Account.CustomerId is null)
        //{
        //    MessageBox.Show("Please select a customer.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    return;
        //}

        var response = await accountService.CreateAccount(Account);

        if (!response.Success)
        {
            // Convert response.ValidationErrors / Message to UI validation as you did for customers
            // Example: show messagebox for now
            var msg = response.Message ?? "Could not create account.";
            System.Windows.MessageBox.Show(msg, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            return;
        }

        // success -> close window
        System.Windows.Application.Current.Windows
            .OfType<System.Windows.Window>()
            .FirstOrDefault(w => w.DataContext == this)?
            .Close();
    }
    private async Task LoadCustomersAsync()
    {
        var customers = await customerService.GetCustomers();
        Customers.Clear();
        foreach (var c in customers)
            Customers.Add(c);
    }
}
