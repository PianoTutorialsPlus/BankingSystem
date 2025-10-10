using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task SaveChangesAsync();
}
