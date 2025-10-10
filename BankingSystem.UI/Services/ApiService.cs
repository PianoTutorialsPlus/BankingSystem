using BankingSystem.Application.DTOs;
using BankingSystem.UI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BankingSystem.UI.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/") // change if API runs elsewhere
        };
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private async Task<T> ReadAsync<T>(HttpResponseMessage res)
    {
        res.EnsureSuccessStatusCode();
        var json = await res.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json) ?? Activator.CreateInstance<T>()!;
    }

    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var res = await _client.GetAsync("api/customers");
        return await ReadAsync<List<CustomerDto>>(res);
    }

    public async Task CreateCustomerAsync(CustomerDto dto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
        var res = await _client.PostAsync("api/customers", content);
        res.EnsureSuccessStatusCode();
    }

    public async Task UpdateCustomerAsync(int id, CustomerDto dto)
    {
        var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
        var res = await _client.PutAsync($"api/customers/{id}", content);
        res.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var res = await _client.DeleteAsync($"api/customers/{id}");
        res.EnsureSuccessStatusCode();
    }

    public async Task<List<AccountDto>> GetAccountsAsync()
    {
        var res = await _client.GetAsync("api/accounts"); // create an endpoint or adapt
        if (!res.IsSuccessStatusCode) return new List<AccountDto>();
        return await ReadAsync<List<AccountDto>>(res);
    }

    public async Task CreateAccountAsync(CreateAccountRequest request)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var res = await _client.PostAsync("api/accounts", content);
        res.EnsureSuccessStatusCode();
    }

    public async Task<List<TransactionDto>> GetTransactionsByAccountAsync(int accountId)
    {
        var res = await _client.GetAsync($"api/accounts/{accountId}/transactions");
        if (!res.IsSuccessStatusCode) return new List<TransactionDto>();
        return await ReadAsync<List<TransactionDto>>(res);
    }

    public async Task PerformTransactionAsync(int accountId, PerformTransactionRequest request)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var res = await _client.PostAsync($"api/accounts/{accountId}/transactions", content);
        res.EnsureSuccessStatusCode();
    }
}
