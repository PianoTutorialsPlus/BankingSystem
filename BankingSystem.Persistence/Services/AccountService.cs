using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Persistence.Services;

public class AccountService
{
    private readonly BankingDbContext _ctx;
    private readonly IChecksumService _checksum;

    public AccountService(BankingDbContext ctx, IChecksumService checksum)
    {
        _ctx = ctx;
        _checksum = checksum;
    }

    public async Task<int> CreateAccountAsync(int customerId, decimal openingBalance, CancellationToken ct = default)
    {
        // generate account number
        var acc = new Account
        {
            AccountNumber = "AC" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 999),
            CustomerId = customerId,
            OpeningBalance = openingBalance,
            CurrentBalance = openingBalance,
            CreatedAt = DateTime.UtcNow
        };

        await _ctx.Accounts.AddAsync(acc, ct);
        await _ctx.SaveChangesAsync(ct);

        // compute checksum (include persisted id)
        acc.BalanceChecksum = _checksum.Compute(acc.Id, acc.CurrentBalance);
        _ctx.Accounts.Update(acc);
        await _ctx.SaveChangesAsync(ct);

        return acc.Id;
    }

    public async Task<int> PerformTransactionAsync(int accountId, DateTime date, decimal amount, string purpose, string iban, string transactionNumber, TransactionType type, CancellationToken ct = default)
    {
        using var tx = await _ctx.Database.BeginTransactionAsync(ct);
        var account = await _ctx.Accounts.FirstOrDefaultAsync(a => a.Id == accountId, ct) ?? throw new Exception("Account not found");

        if (!_checksum.Verify(account.Id, account.CurrentBalance, account.BalanceChecksum))
            throw new Exception("Account balance integrity violated");

        var newBalance = account.CurrentBalance;
        if (type == TransactionType.Deposit || type == TransactionType.Incoming) newBalance += amount;
        else newBalance -= amount;

        // create transaction
        var t = new Transaction
        {
            AccountId = accountId,
            TransactionDate = date,
            Amount = amount,
            Purpose = purpose,
            IBAN = iban,
            TransactionNumber = transactionNumber,
            Type = type,
            CreatedAt = DateTime.UtcNow,
            IsArchived = false
        };

        await _ctx.Transactions.AddAsync(t, ct);

        account.CurrentBalance = newBalance;
        account.BalanceChecksum = _checksum.Compute(account.Id, account.CurrentBalance);
        _ctx.Accounts.Update(account);

        await _ctx.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return t.Id;
    }

    public async Task ArchiveTransactionAsync(int txId, CancellationToken ct = default)
    {
        var tx = await _ctx.Transactions.FindAsync(new object[] { txId }, ct) ?? throw new Exception("Transaction not found");
        tx.IsArchived = true;
        _ctx.Transactions.Update(tx);
        await _ctx.SaveChangesAsync(ct);
        // balance unchanged
    }
}
