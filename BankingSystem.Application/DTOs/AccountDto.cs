namespace BankingSystem.Application.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal CurrentBalance { get; set; }
}
