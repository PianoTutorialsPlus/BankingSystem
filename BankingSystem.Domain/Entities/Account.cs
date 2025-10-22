using BankingSystem.Domain.Common;

namespace BankingSystem.Domain.Entities;

public class Account : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
