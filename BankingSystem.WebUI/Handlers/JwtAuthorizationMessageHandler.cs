using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BankingSystem.WebUI.Handlers;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService localStorageService;

    public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await localStorageService.GetItemAsync<string>("token");
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
