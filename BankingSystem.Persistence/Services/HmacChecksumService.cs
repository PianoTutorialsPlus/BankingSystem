using System.Security.Cryptography;
using System.Text;
using BankingSystem.Application.Contracts;
using BankingSystem.Application.Interfaces;

namespace BankingSystem.Persistence.Services;

public class HmacChecksumService : IChecksumService
{
    private readonly byte[] _secret;

    public HmacChecksumService(string secret)
    {
        _secret = Encoding.UTF8.GetBytes(secret ?? "dev-secret");
    }

    public string Compute(int accountId, decimal currentBalance)
    {
        var payload = $"{accountId}:{currentBalance:F2}";
        using var hmac = new HMACSHA256(_secret);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(hash);
    }

    public bool Verify(int accountId, decimal currentBalance, string checksum)
    {
        if (string.IsNullOrEmpty(checksum)) return false;
        return Compute(accountId, currentBalance) == checksum;
    }
}
