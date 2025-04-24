namespace E2ETesting.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class LogOut
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 5000 });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser!.CloseAsync();
        _playwright!.Dispose();
    }
    
    [Given(@"I am on the homes page")]
    public async Task GivenIAmOnTheHomesPage()
    {
    await _page!.GotoAsync("http://localhost:3000/signin");
    await _page.FillAsync("input[name=\"username\"]", "admin");
    await _page.FillAsync("input[name=\"password\"]", "barca");
    await _page.ClickAsync("button:has-text(\"Logga in\")");
    await _page.WaitForURLAsync("http://localhost:3000/homes");
    }
    
    [When(@"I click the logout button")]
    public async Task WhenIClickTheLogoutButton()
    
    {
    await _page!.WaitForSelectorAsync("button:has-text(\"Logga ut\")");
    await _page!.ClickAsync("button:has-text(\"Logga ut\")");
    }
    
    [Then(@"I should be logged out")]
    public async Task ThenIShouldBeLoggedOut()
    {
        await _page!.WaitForURLAsync("http://localhost:3000/signin");
        Assert.Contains("/signin", _page!.Url);
    }
}