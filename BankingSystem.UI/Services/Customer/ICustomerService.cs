using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.Services.Customer
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetCustomers();
        Task<Response<Guid>> CreateCustomer(CustomerVM customer);
    }
}