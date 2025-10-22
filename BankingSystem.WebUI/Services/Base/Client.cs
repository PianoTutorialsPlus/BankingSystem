namespace BankingSystem.WebUI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient => _httpClient;
}
