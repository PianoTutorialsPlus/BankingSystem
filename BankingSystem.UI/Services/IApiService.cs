using BankingSystem.Application.DTOs;
using BankingSystem.UI.Models;

namespace BankingSystem.UI.Services;

public interface IApiService
{
    Task<List<CustomerDto>> GetCustomersAsync();
    Task CreateCustomerAsync(CustomerDto dto);
    Task UpdateCustomerAsync(int id, CustomerDto dto);
    Task DeleteCustomerAsync(int id);

    Task<List<AccountDto>> GetAccountsAsync();
    Task CreateAccountAsync(CreateAccountRequest request);

    Task<List<TransactionDto>> GetTransactionsByAccountAsync(int accountId);
    Task PerformTransactionAsync(int accountId, PerformTransactionRequest request);
}
