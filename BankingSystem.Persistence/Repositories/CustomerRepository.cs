using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Domain.Entities;
using BankingSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankingDbContext context) : base(context)
        {
        }

        public async Task<bool> IsCustomerUnique(string firstName, string lastName, int id = -1)
        {
            return !await context.Customers.AnyAsync(c => c.FirstName == firstName && c.LastName == lastName && c.Id != id);
        }
    }
}
