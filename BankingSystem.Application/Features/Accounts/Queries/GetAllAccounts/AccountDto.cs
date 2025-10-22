namespace BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;

public class AccountDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
