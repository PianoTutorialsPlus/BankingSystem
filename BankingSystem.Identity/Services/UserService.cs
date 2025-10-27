using BankingSystem.Application.Contracts.Identity;
using BankingSystem.Application.Models.Identity;
using BankingSystem.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BankingSystem.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IHttpContextAccessor contextAccessor;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor)
    {
        this.userManager = userManager;
        this.contextAccessor = contextAccessor;
    }

    public string UserId =>
            contextAccessor.HttpContext?.User?.FindFirstValue("uid");

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(employee => new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        }).ToList();
    }
}