using BankingSystem.Application.Features.Customers.Commands.CreateCustomer;
using BankingSystem.Application.Features.Customers.Commands.DeleteCustomer;
using BankingSystem.Application.Features.Customers.Commands.UpdateCustomer;
using BankingSystem.Application.Features.Customers.Queries.GetAllCustomers;
using BankingSystem.Application.Features.Customers.Queries.GetCustomerDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> Get()
    {
        var customers = await mediator.Send(new GetAllCustomersQuery());

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> Get(int id)
    {
        var customer = await mediator.Send(new GetCustomerDetailsQuery(id));

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(CreateCustomerCommand customer)
    {
        var response = await mediator.Send(customer);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(UpdateCustomerCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteCustomerCommand { Id = id });
        return NoContent();
    }

}

