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
        // Navigate to login page
        await Page.GotoAsync($"{BaseUrl}/login");

        // Wait until Blazor WebAssembly bootstraps (the blazor.webassembly.js finishes execution)
        await Page.WaitForSelectorAsync("script[src*='blazor.webassembly.js']", new() { State = WaitForSelectorState.Detached });

        // Wait until the main layout is rendered (for example, existence of a known element)
        await Page.WaitForSelectorAsync("text=Login", new() { Timeout = 60000 });

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
