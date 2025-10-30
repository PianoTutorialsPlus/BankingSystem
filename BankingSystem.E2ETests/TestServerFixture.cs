using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Playwright;

namespace BankingSystem.E2ETests;

[SetUpFixture]
public class TestServerFixture
{
    public static IHost? ApiHost { get; private set; }
    public static IHost? WebHost { get; private set; }
    public static IPlaywright? PlaywrightInstance;
    public static IBrowser? Browser;
    public static string ApiUrl => ApiHost?.GetTestServer().BaseAddress.ToString() ?? "";
    public static string WebUrl => WebHost?.GetTestServer().BaseAddress.ToString() ?? "";

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        // Start the API in-memory
        ApiHost = TestProgram.BuildHost();
        await ApiHost.StartAsync();

        WebHost = BuildBlazorHost();

        // Initialize Playwright
        PlaywrightInstance = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await PlaywrightInstance.Firefox.LaunchAsync(new()
        {
            Headless = false, // show browser
            SlowMo = 100      // small delay for visual debugging
        });
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await Browser?.CloseAsync()!;
        await ApiHost!.StopAsync();
        await WebHost!.StopAsync();

        WebHost?.Dispose();
        ApiHost?.Dispose();
        PlaywrightInstance?.Dispose();
    }

    public static IHost BuildBlazorHost()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseTestServer();
                webBuilder.Configure(app =>
                {
                    var webUiPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "BankingSystem.WebUI", "wwwroot");
                    app.UseDefaultFiles();
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(webUiPath)
                    });
                });
            })
            .Start();

        return host;
    }
}
