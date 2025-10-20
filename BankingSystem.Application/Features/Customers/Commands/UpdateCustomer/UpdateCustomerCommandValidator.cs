using BankingSystem.Application.Contracts.Repository;
using FluentValidation;

namespace BankingSystem.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;

    public UpdateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        RuleFor(x => x.Id).NotNull().MustAsync(CustomerMustExist);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.EmailAddress).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.EmailAddress));
        RuleFor(x => x.ZipCode)
            .Matches(@"^\d{4,5}$")
            .When(x => !string.IsNullOrWhiteSpace(x.ZipCode))
            .WithMessage("Invalid ZIP code format.");

        RuleFor(x => x)
            .MustAsync(CustomerNameUnique)
            .WithMessage("Customer already Exists");

        this.customerRepository = customerRepository;
    }

    private async Task<bool> CustomerNameUnique(UpdateCustomerCommand command, CancellationToken token)
    {
        return await customerRepository.IsCustomerUnique(command.FirstName, command.LastName, command.Id);
    }

    private async Task<bool> CustomerMustExist(int id, CancellationToken token)
    {
        return await customerRepository.GetByIdAsync(id) != null;
    }
}
