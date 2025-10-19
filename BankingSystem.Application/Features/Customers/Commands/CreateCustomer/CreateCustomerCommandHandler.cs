using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Exceptions;
using BankingSystem.Domain.Entities;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public CreateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        customerRepository = repo;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        // validation
        var validator = new CreateCustomerCommandValidator();
        var result = await validator.ValidateAsync(request);

        if (result.Errors.Any())
            throw new BadRequestException("Invalid Customer", result);

        var data = mapper.Map<Customer>(request);

        await customerRepository.CreateAsync(data);
        
        return data.Id;
    }
}
