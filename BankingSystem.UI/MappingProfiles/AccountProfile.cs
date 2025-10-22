using AutoMapper;
using BankingSystem.UI.Models.Account;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountDto, AccountVM>().ReverseMap();
        CreateMap<CreateAccountCommand, AccountVM>().ReverseMap();
    }
}
