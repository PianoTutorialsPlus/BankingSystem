using BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Queries.GetCustomerDetails
{
    public record GetCustomerDetailsQuery(int Id) : IRequest<CustomerDto>;
}
