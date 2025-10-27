using System.ComponentModel.DataAnnotations;

namespace BankingSystem.WebUI.Models;

public class EmployeeVM
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
