using BankingSystem.WebUI.Models.Customer;
using BankingSystem.WebUI.Services.Customer;
using Microsoft.AspNetCore.Components;

namespace BankingSystem.WebUI.Pages.Customers
{
    public partial class CreateCustomer
    {

        private CustomerVM Customer = new();

        [Inject]
        public ICustomerService CustomerService { get; set; }

        private async Task HandleSubmit()
        {
            await CustomerService.CreateCustomer(Customer);
            Nav.NavigateTo("/customers");
        }
    }
}