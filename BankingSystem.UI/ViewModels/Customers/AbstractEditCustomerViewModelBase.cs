using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Customer;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.UI.ViewModels.Customers;

public abstract class AbstractEditCustomerViewModelBase
{
    protected readonly ICustomerService customerService;

    public CustomerVM Customer { get; set; } = new();

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected AbstractEditCustomerViewModelBase(ICustomerService customerService)
    {
        this.customerService = customerService;
        SaveCommand = new RelayCommand(async _ => await SaveAsync());
        CancelCommand = new RelayCommand(_ => CancelAsync());
    }

    protected virtual void CancelAsync()
    {
        CloseWindow();
    }

    protected abstract Task SaveAsync();

    protected void CloseWindow()
    {
        System.Windows.Application.Current.Windows
            .OfType<Window>()
            .FirstOrDefault(w => w.DataContext == this)?
            .Close();
    }
}
