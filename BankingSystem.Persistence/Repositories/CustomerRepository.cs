using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Domain.Entities;
using BankingSystem.Persistence.DatabaseContext;

namespace BankingSystem.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankingDbContext context) : base(context)
        {
        }
    }
}
