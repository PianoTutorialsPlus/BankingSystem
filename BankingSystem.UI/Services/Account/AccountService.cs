using AutoMapper;
using BankingSystem.UI.Models.Account;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.Services.Account;

public class AccountService : BaseHttpService, IAccountService
{
    private readonly IMapper mapper;

    public AccountService(IClient client, IMapper mapper) : base(client)
    {
        this.mapper = mapper;
    }

    public async Task<List<AccountVM>> GetAccounts()
    {
        var accounts = await client.AccountsAllAsync();
        return mapper.Map<List<AccountVM>>(accounts);
    }

    public async Task<Response<Guid>> CreateAccount(AccountVM account)
    {
        try
        {
            var command = mapper.Map<CreateAccountCommand>(account);

            await client.AccountsPOSTAsync(command);

            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteAccount(int id)
    {
        try
        {
            await client.AccountsDELETEAsync(id);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
