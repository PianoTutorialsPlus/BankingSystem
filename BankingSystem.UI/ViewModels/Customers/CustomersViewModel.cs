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
    public ICommand UpdateCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService, INavigationService navigationService)
    {
        this.customerService = customerService;
        this.navigationService = navigationService;

        CreateCustomerCommand = new RelayCommand(async _ => await CreateCustomerAsync());
        UpdateCustomerCommand = new RelayCommand(async _ => await UpdateCustomerAsync(), _ => SelectedCustomer != null);
        _ = UpdateUI();
    }

    private CustomerVM? selectedCustomer;
    public CustomerVM? SelectedCustomer
    {
        get => selectedCustomer;
        set
        {
            if (selectedCustomer != value)
            {
                selectedCustomer = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateCustomerCommand).RaiseCanExecuteChanged();
            }
        }
    }
    public async Task UpdateUI()
    {
        var customers = await customerService.GetCustomers();

        Customers.Clear();
        foreach (var customer in customers) Customers.Add(customer);

        OnPropertyChanged(nameof(Customers));
        SelectedCustomer = null;
    }
    private async Task UpdateCustomerAsync()
    {
        if (SelectedCustomer is null)
            return;

        navigationService.OpenWindow<UpdateCustomerViewModel, CustomerVM>(SelectedCustomer);
        await UpdateUI();
    }

    private async Task CreateCustomerAsync()
    {
        navigationService.OpenWindow<CreateCustomerViewModel>();
        await UpdateUI();
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

