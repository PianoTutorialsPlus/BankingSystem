using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Domain.Entities;
using BankingSystem.Persistence.DatabaseContext;
using BankingSystem.Persistence.Repositories;

namespace BankingSystem.Infrastructure.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(BankingDbContext context) : base(context) { }
}
