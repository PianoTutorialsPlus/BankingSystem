namespace BankingSystem.Application.Interfaces;

public interface IChecksumService
{
    string Compute(int accountId, decimal currentBalance);
    bool Verify(int accountId, decimal currentBalance, string checksum);
}
