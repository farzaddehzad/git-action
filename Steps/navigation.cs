namespace E2ETesting.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class LoginScenario
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

    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [Given("I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        await _page.GotoAsync("http://localhost:3000/signin");
    }

    [When(@"I enter ""(.)"" as the username")]
    public async Task WhenIEnterAsTheUsername(string username)
    {
        await _page.FillAsync(".inputField", username);
    }

    [When(@"I enter ""(.)"" as the password")]
    public async Task WhenIEnterAsThePassword(string password)
    {
        await _page.FillAsync(".inputField-lösenord", password);
    }

    [When("I click the login button")]
    public async Task WhenIClickTheLoginButton()
    {
        await _page.ClickAsync(".SigninButton-signin");
    }

    [Then("I should be logged in successfully")]
    public async Task ThenIShouldBeLoggedInSuccessfully()
    {

        await _page.WaitForURLAsync("**/homes");
        var currentUrl = _page.Url;
        Assert.True(currentUrl.Contains("/homes"), "User should be redirected to the homes page after login");
    }
}