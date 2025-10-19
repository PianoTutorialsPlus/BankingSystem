using BankingSystem.Application.Contracts.Repository;
using FluentValidation;

namespace BankingSystem.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.EmailAddress)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.EmailAddress));

            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{4,5}$").When(x => !string.IsNullOrWhiteSpace(x.ZipCode))
                .WithMessage("Invalid ZIP code format.");
        }
    }
}