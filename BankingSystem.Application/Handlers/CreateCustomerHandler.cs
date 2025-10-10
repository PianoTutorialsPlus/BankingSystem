using BankingSystem.Application.Commands;
using BankingSystem.Application.CQRS;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Handlers;

public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _repo;
    public CreateCustomerHandler(ICustomerRepository repo) { _repo = repo; }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var c = new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Street = request.Street,
            HouseNumber = request.HouseNumber,
            ZipCode = request.ZipCode,
            City = request.City,
            PhoneNumber = request.PhoneNumber,
            EmailAddress = request.EmailAddress,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(c);
        await _repo.SaveChangesAsync();
        return c.Id;
    }
}
