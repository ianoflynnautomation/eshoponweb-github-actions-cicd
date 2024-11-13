using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.PageObjects;
using Playwright.DotNet.Waits;

namespace Playwright.Dotnet.PageObjects.LoginPage;

/// <summary>
/// Represents the login page of the eShopOnWeb application.
/// </summary>
public class LoginPage : WebPage, ILoginPage
{
    private TextInput EmailInput => App.Component.CreateByDataTestId<TextInput>("login-email-input").ToBeVisible().ToBeClickable();
    private TextInput PasswordInput => App.Component.CreateByDataTestId<TextInput>("login-password-input").ToBeVisible().ToBeClickable();
    private CheckBox RememberMeCheckbox => App.Component.CreateByDataTestId<CheckBox>("login-rememberme-checkbox").ToBeClickable();
    private Button LoginButton => App.Component.CreateByDataTestId<Button>("login-button").ToBeClickable();

    public override string Url => throw new NotImplementedException();

    // <inheritdoc/>
    public async Task<ILoginPage> CheckRememberMe()
    {
        await RememberMeCheckbox.CheckAsync();

        return this;
    }

    // <inheritdoc/>
    public async Task<ILoginPage> Login(string email, string password, bool rememberMe = false)
    {
        await SetEmail(email);
        await SetPassword(password);

        if (rememberMe)
        {
            await CheckRememberMe();
        }

        await LoginButton.ClickAsync();

        return this;
    }

    // <inheritdoc/>
    public async Task<ILoginPage> SetEmail(string email)
    {
        await EmailInput.SetTextAsync(email);

        return this;
    }

    // <inheritdoc/>
    public async Task<ILoginPage> SetPassword(string password)
    {
        await PasswordInput.SetTextAsync(password);

        return this;
    }
    
}
