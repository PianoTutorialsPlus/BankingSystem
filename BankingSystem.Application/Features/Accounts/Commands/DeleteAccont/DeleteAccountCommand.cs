using MediatR;

namespace BankingSystem.Application.Features.Accounts.Commands.DeleteAccont;

public class DeleteAccountCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
