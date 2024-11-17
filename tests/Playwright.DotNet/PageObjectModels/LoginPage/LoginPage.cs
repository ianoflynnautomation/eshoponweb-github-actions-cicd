using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.LoginPage;

public class LoginPage(IPage page) : WebPage(page), ILoginPage
{
    private ILocator EmailInput => Page.Locator("[data-testid=login-email-input]");
    private ILocator PasswordInput => Page.Locator("[data-testid=login-password-input]");
    private ILocator RememberMeCheckbox => Page.Locator("[data-testid=login-rememberme-checkbox]");
    private ILocator LoginButton => Page.Locator("[data-testid=login-button]");

    // <inheritdoc/>
    public async Task CheckRememberMe()
    {
        await RememberMeCheckbox.CheckAsync();

    }

    // <inheritdoc/>
    public async Task Login(string email, string password, bool rememberMe = false)
    {
        await SetEmail(email);
        await SetPassword(password);

        if (rememberMe)
        {
            await CheckRememberMe();
        }

        await LoginButton.ClickAsync();
    }

    // <inheritdoc/>
    public async Task SetEmail(string email)
    {
        await EmailInput.FillAsync(email);

    }

    // <inheritdoc/>
    public async Task SetPassword(string password)
    {
        await PasswordInput.FillAsync(password);

    }

}
