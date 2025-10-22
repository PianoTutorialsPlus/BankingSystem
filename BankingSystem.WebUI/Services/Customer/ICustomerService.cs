using BankingSystem.WebUI.Models.Customer;
using BankingSystem.WebUI.Services.Base;

namespace BankingSystem.WebUI.Services.Customer;

public interface ICustomerService
{
    Task<List<CustomerVM>> GetCustomers();
    Task<Response<Guid>> CreateCustomer(CustomerVM customer);
    //Task<Response<Guid>> UpdateCustomer(CustomerVM customer);
    //Task<Response<Guid>> DeleteCustomer(int id);
}