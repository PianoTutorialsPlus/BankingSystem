using BankingSystem.WebUI.Models;
using BankingSystem.WebUI.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace BankingSystem.WebUI.Pages
{
	public partial class Login
	{
		public LoginVM Model { get; set; }
		public string Message { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }

		protected override void OnInitialized()
		{
			Model = new LoginVM();
		}
		protected async Task HandleLogin()
		{
			if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
			{
				NavigationManager.NavigateTo("/");
			}
			Message = "Username/Password combination unknown";
		}
	}
}