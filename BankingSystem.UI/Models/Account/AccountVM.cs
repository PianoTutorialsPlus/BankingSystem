namespace BankingSystem.UI.Models.Account;

public class AccountVM : AbstractDtoValidationBase
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public double InitialDeposit { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public double Balance { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

}
