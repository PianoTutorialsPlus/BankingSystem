using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Exceptions;
using BankingSystem.Domain.Entities;
using MediatR;
using System.Security.Principal;

namespace BankingSystem.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
    private readonly IAccountRepository accountRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateAccountCommand request, CancellationToken ct)
    {
        var validator = new CreateAccountCommandValidator(customerRepository);
        var result = await validator.ValidateAsync(request, ct);

        if (result.Errors.Any())
            throw new BadRequestException("Invalid account data", result);

        var data = mapper.Map<Account>(request);
        data.AccountNumber = GenerateAccountNumber();

        await accountRepository.CreateAsync(data);

        return data.Id;
    }

    private static string GenerateAccountNumber()
    {
        return $"AC-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
    }
}
