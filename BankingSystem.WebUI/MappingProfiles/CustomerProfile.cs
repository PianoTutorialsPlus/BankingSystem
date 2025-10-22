using AutoMapper;
using BankingSystem.WebUI.Models.Customer;
using BankingSystem.WebUI.Services.Base;

namespace BankingSystem.WebUI.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, CustomerVM>().ReverseMap();
            CreateMap<CreateCustomerCommand, CustomerVM>().ReverseMap();
            CreateMap<UpdateCustomerCommand, CustomerVM>().ReverseMap();
        }
    }
}
