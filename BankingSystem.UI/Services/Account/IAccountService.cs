using BankingSystem.UI.Models.Account;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.Services.Account
{
    public interface IAccountService
    {
        Task<List<AccountVM>> GetAccounts();
        Task<Response<Guid>> CreateAccount(AccountVM account);
    }
}