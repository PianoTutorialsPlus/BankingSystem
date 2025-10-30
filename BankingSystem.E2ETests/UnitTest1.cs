using Microsoft.Playwright;

namespace BankingSystem.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : PageTest
{
    private const string BaseUrl = "https://localhost:7247"; // or your test environment

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

        // Validate we landed on the welcome page
        var heading = await Page.TextContentAsync("h1");
        Assert.That(heading, Does.Contain("Welcome to Banking System"));
    }
}
