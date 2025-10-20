using BankingSystem.Application.Contracts.Repository;
using FluentValidation;

namespace BankingSystem.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.EmailAddress)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.EmailAddress));

            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{4,5}$").When(x => !string.IsNullOrWhiteSpace(x.ZipCode))
                .WithMessage("Invalid ZIP code format.");

            RuleFor(x => x)
                .MustAsync(CustomerNameUnique)
                .WithMessage("Customer already exists");

            this.customerRepository = customerRepository;
        }

        private async Task<bool> CustomerNameUnique(CreateCustomerCommand command, CancellationToken token)
        {
            return await customerRepository.IsCustomerUnique(command.FirstName, command.LastName);
        }
    }
}