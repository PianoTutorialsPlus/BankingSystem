using Microsoft.Playwright;
using NUnit.Framework;

namespace BankingSystem.E2ETests
{
    [TestFixture]
    public class LoginTests
    {
        private IPage _page = null!;
        private IBrowserContext _context = null!;
        private string BaseUrl => TestServerFixture.WebUrl;

        [SetUp]
        public async Task Setup()
        {
            _context = await TestServerFixture.Browser!.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _context.CloseAsync();
        }

        [Test]
        public async Task Should_Login_Successfully()
        {
            await _page.GotoAsync($"{BaseUrl}login");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await _page.WaitForSelectorAsync("#userName");
            await _page.FillAsync("#userName", "user@localhost.com");
            await _page.FillAsync("#password", "P@ssword1");

            await _page.ClickAsync("input[value='Login']");
            await _page.WaitForURLAsync("**/");

            var heading = await _page.TextContentAsync("h1");
            Assert.That(heading, Does.Contain("Welcome"));
        }
    }
}
