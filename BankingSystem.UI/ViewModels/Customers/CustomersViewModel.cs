using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Navigation;
using BankingSystem.UI.Services.Customer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels.Customers;

public class CustomersViewModel : INotifyPropertyChanged
{
    private readonly ICustomerService customerService;
    private readonly INavigationService navigationService;

    public ObservableCollection<CustomerVM> Customers { get; } = new();

    public ICommand CreateCustomerCommand { get; }
    public ICommand UpdateCustomerCommand { get; }
    public ICommand DeleteCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService, INavigationService navigationService)
    {
        this.customerService = customerService;
        this.navigationService = navigationService;

        CreateCustomerCommand = new RelayCommand(async _ => await CreateCustomerAsync());
        UpdateCustomerCommand = new RelayCommand(async _ => await UpdateCustomerAsync(), _ => SelectedCustomer != null);
        DeleteCustomerCommand = new RelayCommand(async _ => await DeleteCustomerAsync(), _ => SelectedCustomer != null);

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
                ((RelayCommand)DeleteCustomerCommand).RaiseCanExecuteChanged();
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
    private async Task CreateCustomerAsync()
    {
        navigationService.OpenWindow<CreateCustomerViewModel>();
        await UpdateUI();
    }
    private async Task UpdateCustomerAsync()
    {
        if (SelectedCustomer is null)
            return;

        navigationService.OpenWindow<UpdateCustomerViewModel, CustomerVM>(SelectedCustomer);
        await UpdateUI();
    }
    private async Task DeleteCustomerAsync()
    {
        if (SelectedCustomer is null)
            return;

        var result = MessageBox.Show(
            $"Are you sure you want to delete {SelectedCustomer.FirstName} {SelectedCustomer.LastName}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
        );

        if (result != MessageBoxResult.Yes)
            return;

        var response = await customerService.DeleteCustomer(SelectedCustomer.Id);

        if (response.Success)
            await UpdateUI();
        else
            MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}

