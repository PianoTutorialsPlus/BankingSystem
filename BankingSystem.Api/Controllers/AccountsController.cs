using BankingSystem.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly BankingDbContext _ctx;
    private readonly BankingSystem.Persistence.Services.AccountService _accService;

    public AccountsController(BankingDbContext ctx, BankingSystem.Persistence.Services.AccountService accService)
    {
        _ctx = ctx;
        _accService = accService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequest req)
    {
        var cust = await _ctx.Customers.FindAsync(req.CustomerId);
        if (cust == null) return BadRequest("Customer not found");
        var id = await _accService.CreateAccountAsync(req.CustomerId, req.OpeningBalance);
        return CreatedAtAction(nameof(Get), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var a = await _ctx.Accounts.FindAsync(id);
        if (a == null) return NotFound();
        return Ok(new { a.Id, a.AccountNumber, a.CustomerId, a.OpeningBalance, a.CurrentBalance });
    }

    [HttpPost("{id}/transactions")]
    public async Task<IActionResult> PostTransaction(int id, [FromBody] PerformTransactionRequest req)
    {
        var acc = await _ctx.Accounts.FindAsync(id);
        if (acc == null) return NotFound("Account not found");

        var txId = await _accService.PerformTransactionAsync(id, req.Date, req.Amount, req.Purpose, req.IBAN, req.TransactionNumber, (Domain.Enums.TransactionType)req.Type);
        return Ok(new { id = txId });
    }

    [HttpPost("archiveTransaction/{txId}")]
    public async Task<IActionResult> ArchiveTransaction(int txId)
    {
        await _accService.ArchiveTransactionAsync(txId);
        return NoContent();
    }
}

public record CreateAccountRequest(int CustomerId, decimal OpeningBalance);
public record PerformTransactionRequest(DateTime Date, decimal Amount, string Purpose, string IBAN, string TransactionNumber, int Type);

