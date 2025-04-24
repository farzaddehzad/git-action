namespace E2ETesting.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class LogIn
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 900 });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser!.CloseAsync();
        _playwright!.Dispose();
    }

    [Given(@"I am on the signin page")]
    public async Task GivenIAmOnTheSigninPage()
    {
        await _page!.GotoAsync("http://localhost:3000/signin");
    }

    [When(@"I enter ""(.)"" as the username")]
    public async Task WhenIEnterAsTheUsername(string username)
    {
        await _page!.FillAsync("input[placeholder='Användarnamn']", username);
    }

    [When(@"I enter ""(.)"" as the password")]
    public async Task WhenIEnterAsThePassword(string password)
    {
        await _page!.FillAsync("input[placeholder='password']", password);
    }

    [When(@"I press on the login button")]
    public async Task WhenIPressOnTheLoginButton()
    {
        await _page!.ClickAsync(".SigninButton-signin");
    }

    [Then(@"I should be logged in")]
    public async Task ThenIShouldBeLoggedIn()
    {
        await _page!.WaitForTimeoutAsync(1000);
        //await _page!.WaitForURLAsync("http://localhost:3000/homes");
        Assert.Contains("/homes", _page!.Url);
    }
}