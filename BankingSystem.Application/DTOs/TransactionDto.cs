namespace BankingSystem.Application.DTOs;

public class TransactionDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string IBAN { get; set; } = string.Empty;
    public string TransactionNumber { get; set; } = string.Empty;
    public int Type { get; set; }
    public bool IsArchived { get; set; }
}
