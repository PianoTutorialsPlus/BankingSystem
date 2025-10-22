using BankingSystem.Application.Features.Accounts.Commands.CreateAccount;
using BankingSystem.Application.Features.Accounts.Queries;
using BankingSystem.Application.Features.Accounts.Queries.GetAccountDetails;
using BankingSystem.Application.Features.Accounts.Queries.GetAllAccounts;
using BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;
using BankingSystem.Application.Features.Customers.Queries.GetCustomerDetails;
using BankingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountDto>>> Get()
    {
        var customers = await mediator.Send(new GetAllAccountsQuery());

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> Get(int id)
    {
        var customer = await mediator.Send(new GetAccountDetailsQuery(id));

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateAccountCommand account)
    {
        var response = await mediator.Send(account);
        return CreatedAtAction(nameof(Get), new { id = response });
    }
}
