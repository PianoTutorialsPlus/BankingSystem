using AutoMapper;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;
using MediatR;

namespace BankingSystem.Application.Features.Customers.Queries.GetCustomerDetails
{
    public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, CustomerDto>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public GetCustomerDetailsQueryHandler(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByIdAsync(request.Id);

            var data = mapper.Map<CustomerDto>(customer);

            return data;
        }
    }
}
