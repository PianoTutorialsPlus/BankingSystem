using MediatR;

namespace BankingSystem.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
