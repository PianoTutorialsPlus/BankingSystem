namespace BankingSystem.UI.Models.Customer;

public class CustomerVM : AbstractDtoValidationBase 
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Street { get; set; } = "";
    public string HouseNumber { get; set; } = "";
    public string ZipCode { get; set; } = "";
    public string City { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string FullName => $"{FirstName} {LastName}";
}

