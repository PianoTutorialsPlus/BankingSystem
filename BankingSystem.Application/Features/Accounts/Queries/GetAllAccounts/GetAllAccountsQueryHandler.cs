using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetAsync();

        var data = mapper.Map<List<AccountDto>>(accounts);

        return data;
    }
}
