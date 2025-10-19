using AutoMapper;
using BankingSystem.Application.Features.Customers.Commands.CreateCustomer;
using BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>();
        }
    }
}
