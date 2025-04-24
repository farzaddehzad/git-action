namespace E2ETesting.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class Analyspage
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 200 });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();

        await _page.GotoAsync("http://localhost:3000/signin");
        await _page.FillAsync(".inputField", "admin");
        await _page.FillAsync(".inputField-lösenord", "barca");
        await _page.ClickAsync(".SigninButton-signin");
        await _page.WaitForURLAsync("/homes");
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [When("I click the analys button")]
    public async Task ClickTheAnalysisButton()
    {
        await _page.ClickAsync(".analys-link");
    }

    [Then("I should be redirected to the analys page")]
    public async Task ThenIShouldBeRedirectedToTheAnalysisPage()
    {
        await _page.WaitForURLAsync("/analys");
        var currentUrl = _page.Url;
        Assert.True(currentUrl.Contains("/analys"), "User should be redirected to the analys page");
    }
}