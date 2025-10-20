using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Contracts.Repository;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<bool> IsCustomerUnique(string firstName, string lastName, int id = -1);
}
