
using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.Validations;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.PageObjects.Sections;

/// <summary>
/// Represents the header section of the eShopOnWeb application.
/// </summary>
public class HeaderSection : WebPage, IHeaderSection
{
    private Image BasketButton => App.Component.CreateByXpath<Image>("img[src='/images/cart.png']").ToBeClickable();
    private Button LoginButton => App.Component.CreateByDataTestId<Button>("identity-login-button").ToBeVisible().ToBeClickable();
    private Div UserEmail => App.Component.CreateByDataTestId<Div>("logged-in-user-email").ToBeVisible();

    public override string Url => throw new NotImplementedException();

    // <inheritdoc/>
    public async Task<IHeaderSection> OpenBasket()
    {
        await BasketButton.ClickAsync();

        return this;
    }

    // <inheritdoc/>
    public async Task<IHeaderSection> OpenLogin()
    {
        await LoginButton.ClickAsync();

        return this;
    }

    // <inheritdoc/>
    public IHeaderSection UserEmailShouldBe(string email)
    {
        UserEmail.ValidateInnerTextIs(email);

        return this;
    }

}