using MediatR;

namespace BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;
