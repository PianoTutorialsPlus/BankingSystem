using MediatR;

namespace BankingSystem.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public decimal InitialDeposit { get; set; }
}
