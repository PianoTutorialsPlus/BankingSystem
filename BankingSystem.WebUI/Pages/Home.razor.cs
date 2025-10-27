using BankingSystem.WebUI.Providers;
using BankingSystem.WebUI.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BankingSystem.WebUI.Pages
{
    public partial class Home
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
        }
        protected void GoToLogin()
        {
            NavigationManager.NavigateTo("login/");
        }
        protected void GoToRegister()
        {
            NavigationManager.NavigateTo("register/");
        }
        protected async void Logout()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }

        private void GotToCustomers()
        {
            NavigationManager.NavigateTo("customers/");
        }
    }
}