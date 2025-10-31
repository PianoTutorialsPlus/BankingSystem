using Microsoft.Playwright;

namespace BankingSystem.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : PageTest
{
    private string BaseUrl => TestContext.Parameters.Get("E2E_BaseUrl")!; // or your test environment

    [Test]
    public async Task Pre_Should_Login_Successfully()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "page.png", FullPage = true });
        var content = await Page.ContentAsync();
        Console.WriteLine(content.Substring(0, Math.Min(2000, content.Length)));

        var heading = await Page.Locator("h1").TextContentAsync();
        Assert.That(heading, Does.Contain("Welcome to Banking System"));
    }

    [Test]
    public async Task Should_Login_Successfully()
    {
        //var heading = await Page.TextContentAsync("h1");
        //Assert.That(heading, Does.Contain("Welcome to Banking System"));

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
