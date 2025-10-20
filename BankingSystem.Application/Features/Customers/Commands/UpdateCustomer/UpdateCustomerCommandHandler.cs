using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Exceptions;
using BankingSystem.Domain.Entities;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        customerRepository = repo;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken ct)
    {
        var validator = new UpdateCustomerCommandValidator(customerRepository);
        var result = await validator.ValidateAsync(request, ct);

        if (result.Errors.Any())
            throw new BadRequestException("Invalid Customer", result);

        var data = mapper.Map<Customer>(request); // updates the entity
        await customerRepository.UpdateAsync(data);

        return Unit.Value;
    }
}
