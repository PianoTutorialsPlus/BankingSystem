using BankingSystem.Application.CQRS;

namespace BankingSystem.Application.Commands;

public record CreateCustomerCommand(string FirstName, string LastName, string Street, string HouseNumber,
    string ZipCode, string City, string PhoneNumber, string EmailAddress) : ICommand<int>;
