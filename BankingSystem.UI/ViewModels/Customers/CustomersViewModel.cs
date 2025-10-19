using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Navigation;
using BankingSystem.UI.Services.Customer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels.Customers;

public class CustomersViewModel : INotifyPropertyChanged
{
    private readonly ICustomerService customerService;
    private readonly INavigationService navigationService;

    public ObservableCollection<CustomerVM> Customers { get; } = new();

    public ICommand CreateCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService, INavigationService navigationService)
    {
        this.customerService = customerService;
        this.navigationService = navigationService;

        CreateCustomerCommand = new RelayCommand(async _ => await CreateCustomerAsync());
        _ = UpdateUI();
    }

    public async Task UpdateUI()
    {
        var customers = await customerService.GetCustomers();

        Customers.Clear();
        foreach (var customer in customers) Customers.Add(customer);

        OnPropertyChanged(nameof(Customers));
    }

    public async Task CreateCustomerAsync()
    {
        navigationService.OpenWindow<CreateCustomerViewModel>();
        await UpdateUI();
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

