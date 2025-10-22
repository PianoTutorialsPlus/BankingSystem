using BankingSystem.Application.Contracts.Repository;
using FluentValidation;

namespace BankingSystem.Application.Features.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    private readonly ICustomerRepository customerRepository;

    public CreateAccountCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(x => x.CustomerId)
            .MustAsync(CustomerMustExist)
            .WithMessage("Customer does not exist");

        RuleFor(x => x.InitialDeposit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Initial deposit cannot be negative");

        this.customerRepository = customerRepository;
    }

    private async Task<bool> CustomerMustExist(int id, CancellationToken token)
    {
        return await customerRepository.GetByIdAsync(id) != null;
    }
}
