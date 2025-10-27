using BankingSystem.WebUI.Providers;
using BankingSystem.WebUI.Services.Base;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BankingSystem.WebUI.Services.Authentication;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public AuthenticationService(
        IClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
    {
        this.authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            var authenticationRequest = new AuthRequest { Email = email, Password = password };
            var authenticationResponse = await client.LoginAsync(authenticationRequest);

            if (authenticationResponse.Token == string.Empty)
                return false;

            await localStorageService.SetItemAsync("token", authenticationResponse.Token);
            // Set claims in Blazor and login state
            await ((ApiAuthenticationStateProvider)authenticationStateProvider)
                .LoggedIn();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync("token");
        // remove claims in Blazor and invalidate login state
        await ((ApiAuthenticationStateProvider)authenticationStateProvider)
            .LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
    {
        var registrationRequest = new RegistrationRequest
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
            Password = password
        };
        var response = await client.RegisterAsync(registrationRequest);

        return !string.IsNullOrEmpty(response.UserId);
    }
}
