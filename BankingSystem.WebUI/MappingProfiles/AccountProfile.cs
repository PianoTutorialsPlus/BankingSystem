using AutoMapper;
using BankingSystem.WebUI.Models.Account;
using BankingSystem.WebUI.Services.Base;

namespace BankingSystem.WebUI.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDto, AccountVM>().ReverseMap();
        CreateMap<CreateAccountCommand, AccountVM>().ReverseMap();
    }
}
