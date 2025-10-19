using BankingSystem.UI.Services.Customer;

namespace BankingSystem.UI.ViewModels.Customers;

public class CreateCustomerViewModel : AbstractEditCustomerViewModelBase
{
    public CreateCustomerViewModel(ICustomerService customerService) : base(customerService)
    {
    }

    protected override async Task SaveAsync()
    {
        var response = await customerService.CreateCustomer(Customer);

        if (response.Success)
            CloseWindow();

        if (response.ValidationErrors != null)
            foreach (var error in response.ValidationErrors)
                Customer.SetErrors(error.Key, error.Value);
    }
}
