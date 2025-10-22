using MediatR;

namespace BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;

public record GetAllAccountsQuery : IRequest<List<AccountDto>>;
