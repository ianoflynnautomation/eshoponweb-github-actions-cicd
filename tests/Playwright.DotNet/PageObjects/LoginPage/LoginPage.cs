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
    public ILoginPage CheckRememberMe()
    {
        RememberMeCheckbox.Check();

        return this;
    }

    // <inheritdoc/>
    public ILoginPage Login(string email, string password, bool rememberMe = false)
    {
        SetEmail(email);
        SetPassword(password);

        if (rememberMe)
        {
            CheckRememberMe();
        }

        LoginButton.Click();

        return this;
    }

    // <inheritdoc/>
    public ILoginPage SetEmail(string email)
    {
        EmailInput.SetText(email);

        return this;
    }

    // <inheritdoc/>
    public ILoginPage SetPassword(string password)
    {
        PasswordInput.SetText(password);

        return this;
    }
    
}
