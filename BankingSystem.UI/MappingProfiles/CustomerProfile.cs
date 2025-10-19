using AutoMapper;
using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, CustomerVM>().ReverseMap();
            CreateMap<CreateCustomerCommand, CustomerVM>().ReverseMap();
        }
    }
}
