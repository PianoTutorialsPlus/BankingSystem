using BankingSystem.Domain.Enums;

namespace BankingSystem.UI.Models;

public class PerformTransactionRequest
{
    private DateTime utcNow;
    private decimal v1;
    private string v2;
    private string v3;
    private string v4;
    private int v5;

    public PerformTransactionRequest(DateTime utcNow, decimal v1, string v2, string v3, string v4, int v5)
    {
        this.utcNow = utcNow;
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.v4 = v4;
        this.v5 = v5;
    }

    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Purpose { get; set; } = "";
    public string IBAN { get; set; } = "";
    public TransactionType Type { get; set; }
}


