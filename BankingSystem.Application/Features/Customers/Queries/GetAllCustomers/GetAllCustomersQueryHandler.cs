using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;

    public GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetAsync();

        var data = mapper.Map<List<CustomerDto>>(customers);

        return data;
    }
}
