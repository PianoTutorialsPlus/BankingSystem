using BankingSystem.Application.DTOs;
using BankingSystem.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly BankingDbContext _ctx;
    public CustomersController(BankingDbContext ctx) { _ctx = ctx; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _ctx.Customers.AsNoTracking().ToListAsync();
        var dtos = list.Select(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Street = c.Street,
            HouseNumber = c.HouseNumber,
            ZipCode = c.ZipCode,
            City = c.City,
            PhoneNumber = c.PhoneNumber,
            EmailAddress = c.EmailAddress
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var c = await _ctx.Customers.FindAsync(id);
        if (c == null) return NotFound();
        var dto = new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Street = c.Street,
            HouseNumber = c.HouseNumber,
            ZipCode = c.ZipCode,
            City = c.City,
            PhoneNumber = c.PhoneNumber,
            EmailAddress = c.EmailAddress
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerDto dto)
    {
        var c = new Domain.Entities.Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Street = dto.Street,
            HouseNumber = dto.HouseNumber,
            ZipCode = dto.ZipCode,
            City = dto.City,
            PhoneNumber = dto.PhoneNumber,
            EmailAddress = dto.EmailAddress,
            CreatedAt = DateTime.UtcNow
        };

        await _ctx.Customers.AddAsync(c);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CustomerDto dto)
    {
        var c = await _ctx.Customers.FindAsync(id);
        if (c == null) return NotFound();
        c.FirstName = dto.FirstName;
        c.LastName = dto.LastName;
        c.Street = dto.Street;
        c.HouseNumber = dto.HouseNumber;
        c.ZipCode = dto.ZipCode;
        c.City = dto.City;
        c.PhoneNumber = dto.PhoneNumber;
        c.EmailAddress = dto.EmailAddress;
        _ctx.Customers.Update(c);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var c = await _ctx.Customers.FindAsync(id);
        if (c == null) return NotFound();
        _ctx.Customers.Remove(c);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}

