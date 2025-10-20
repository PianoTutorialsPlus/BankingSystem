using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Customer;

namespace BankingSystem.UI.ViewModels.Customers;

public class UpdateCustomerViewModel : AbstractEditCustomerViewModelBase
{
    public UpdateCustomerViewModel(ICustomerService customerService, CustomerVM customer) : base(customerService)
    {
        Customer = customer;
    }

    protected override async Task SaveAsync()
    {
        var response = await customerService.UpdateCustomer(Customer);

        if (response.Success)
            CloseWindow();

        if (response.ValidationErrors != null)
            foreach (var error in response.ValidationErrors)
                Customer.SetErrors(error.Key, error.Value);
    }
}
