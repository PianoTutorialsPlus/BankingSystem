using BankingSystem.WebUI.Services.Base;
using Microsoft.Playwright;
using System.Net.Http;

namespace BankingSystem.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : PageTest
{
    private string BaseUrl => TestContext.Parameters.Get("E2E_BaseUrl")!; // or your test environment
    private  string ApiUrl => TestContext.Parameters.Get("E2E_ApiUrl")!;
    private HttpClient _client = new();

    [Test]
    public async Task Api_Should_Be_Reachable()
    {

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiUrl)
        };
        IClient client = new Client(httpClient);

        await client.HealthAsync(); // or whatever endpoint you have

        var response = await client.LoginAsync(new AuthRequest
        {
            Email = "user@localhost.com",
            Password = "P@ssword1"
        });

        //var response = await _client.GetAsync($"{ApiUrl}/health");
        Assert.That(response.Email == "user@localhost.com", $"API not reachable at {ApiUrl}");
        Assert.Pass("✅ API is reachable via ServiceClient");
    }

    [Test]
    public async Task Pre_Should_Login_Successfully()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "page.png", FullPage = true });
        //var content = await Page.ContentAsync();
        //Console.WriteLine(content.Substring(0, Math.Min(2000, content.Length)));

        var heading = await Page.Locator("h1").TextContentAsync();
        Assert.That(heading, Does.Contain("Welcome to Banking System"));
    }

    [Test]
    public async Task Should_Login_Successfully()
    {
        // Navigate to login page
        await Page.GotoAsync($"{BaseUrl}/login");
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Ensure fields are rendered
        await Expect(Page.Locator("#userName")).ToBeVisibleAsync();
        await Expect(Page.Locator("#password")).ToBeVisibleAsync();

        // Fill in credentials
        await Page.FillAsync("#userName", "user@localhost.com");
        await Page.FillAsync("#password", "P@ssword1");

        // Click login
        await Page.ClickAsync("input[value='Login']");

        // Wait for redirect to home/dashboard
        await Page.WaitForURLAsync("**/");
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "page.png", FullPage = true });

        var heading = string.Empty;
        // Validate we landed on the welcome page
        for (int i = 0; i < 10; i++)
        {
            heading = await Page.Locator("h1").TextContentAsync();
            if (!string.IsNullOrEmpty(heading))
                break;
            await Task.Delay(1000);
        }

        Assert.That(heading, Does.Contain("Welcome to Banking System"));
    }
}
