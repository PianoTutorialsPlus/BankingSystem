using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Queries.GetAccountDetails;

public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, AccountDto>
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public GetAccountDetailsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<AccountDto> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByIdAsync(request.Id);

        var data = mapper.Map<AccountDto>(account);

        return data;
    }
}
