using AutoMapper;
using BankingSystem.Application.Features.Accounts.Commands.CreateAccount;
using BankingSystem.Application.Features.Accounts.Queries;
using BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;
using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.MappingProfiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<CreateAccountCommand, Account>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialDeposit))
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

    }
}
