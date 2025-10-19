using AutoMapper;
using BankingSystem.UI.Models.Customer;
using BankingSystem.UI.Services.Base;

namespace BankingSystem.UI.Services.Customer;

public class CustomerService : BaseHttpService, ICustomerService
{
    private readonly IMapper mapper;

    public CustomerService(IClient client, IMapper mapper) : base(client) 
    {
        this.mapper = mapper;
    }

    public async Task<Response<Guid>> CreateCustomer(CustomerVM customer)
    {
        try
        {
            var command = mapper.Map<CreateCustomerCommand>(customer);

            await client.CustomersPOSTAsync(command);

            return new Response<Guid>()
            {
                Success = true
            };

        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<CustomerVM>> GetCustomers()
    {
        var customers = await client.CustomersAllAsync();
        return mapper.Map<List<CustomerVM>>(customers);
    }
}
