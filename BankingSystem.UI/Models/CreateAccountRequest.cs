namespace BankingSystem.UI.Models;

public class CreateAccountRequest
{
    public int CustomerId { get; set; }
    public decimal OpeningBalance { get; set; }
}

