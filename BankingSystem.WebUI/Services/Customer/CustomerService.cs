using AutoMapper;
using BankingSystem.WebUI.Models.Customer;
using BankingSystem.WebUI.Services.Base;

namespace BankingSystem.WebUI.Services.Customer;

public class CustomerService : BaseHttpService, ICustomerService
{
    private readonly IMapper mapper;

    public CustomerService(IClient client, IMapper mapper) : base(client) 
    {
        this.mapper = mapper;
    }

    public async Task<List<CustomerVM>> GetCustomers()
    {
        var customers = await client.CustomersAllAsync();
        return mapper.Map<List<CustomerVM>>(customers);
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
    //public async Task<Response<Guid>> UpdateCustomer(CustomerVM customer)
    //{
    //    try
    //    {
    //        var command = mapper.Map<UpdateCustomerCommand>(customer);
    //        await client.CustomersPUTAsync(command);

    //        return new Response<Guid>() { Success = true };
    //    }
    //    catch (ApiException ex)
    //    {
    //        return ConvertApiExceptions<Guid>(ex);
    //    }
    //}
    //public async Task<Response<Guid>> DeleteCustomer(int id)
    //{
    //    try
    //    {
    //        await client.CustomersDELETEAsync(id);
    //        return new Response<Guid>() { Success = true };
    //    }
    //    catch (ApiException ex)
    //    {
    //        return ConvertApiExceptions<Guid>(ex);
    //    }
    //}
}
