namespace BankingSystem.UI.Models;

public class CreateAccountRequest
{
    private decimal open;

    public CreateAccountRequest(int customerId, decimal open)
    {
        CustomerId = customerId;
        this.open = open;
    }

    public int CustomerId { get; set; }
    public decimal OpeningBalance { get; set; }
}

