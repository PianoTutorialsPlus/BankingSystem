using BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Queries.GetAccountDetails;

public record GetAccountDetailsQuery(int Id) : IRequest<AccountDto>;
