using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Exceptions;
using BankingSystem.Domain.Entities;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly ICustomerRepository accountRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository repo)
    {
        accountRepository = repo;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken ct)
    {
        var entity = await accountRepository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException(nameof(Customer), request.Id);

        await accountRepository.DeleteAsync(entity);
        return Unit.Value;
    }
}
