using BankingSystem.Domain.Common;
using BankingSystem.Domain.Enums;

namespace BankingSystem.Domain.Entities;

public class Transaction : BaseEntity
{
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string IBAN { get; set; } = string.Empty;
    public string TransactionNumber { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public bool IsArchived { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
