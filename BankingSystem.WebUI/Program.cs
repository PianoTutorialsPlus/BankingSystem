using BankingSystem.WebUI;
using BankingSystem.WebUI.Handlers;
using BankingSystem.WebUI.Providers;
using BankingSystem.WebUI.Services.Authentication;
using BankingSystem.WebUI.Services.Base;
using BankingSystem.WebUI.Services.Customer;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<JwtAuthorizationMessageHandler>();

builder.Services
    .AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("http://127.0.0.1:7178/"))
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
