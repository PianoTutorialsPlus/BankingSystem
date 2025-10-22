using BankingSystem.WebUI.Models.Customer;
using BankingSystem.WebUI.Services.Customer;
using Microsoft.AspNetCore.Components;

namespace BankingSystem.WebUI.Pages.Customers
{
    public partial class Customers
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        public List<CustomerVM> CustomersVM { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            CustomersVM = await CustomerService.GetCustomers();
        }
    }
}