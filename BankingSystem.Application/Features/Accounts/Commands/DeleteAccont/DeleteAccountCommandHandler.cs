using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Exceptions;
using BankingSystem.Domain.Entities;
using MediatR;

namespace BankingSystem.Application.Features.Accounts.Commands.DeleteAccont;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly IAccountRepository accountRepository;

    public DeleteAccountCommandHandler(IAccountRepository repo)
    {
        accountRepository = repo;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken ct)
    {
        var entity = await accountRepository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException(nameof(Account), request.Id);

        await accountRepository.DeleteAsync(entity);
        return Unit.Value;
    }
}
