
using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Sections;

public class HeaderSection(IPage page) : WebPage(page), IHeaderSection
{
    private ILocator BasketButton => Page.Locator("img[src='/images/cart.png']");
    private ILocator LoginButton => Page.Locator("[data-testid=identity-login-button]");
    private ILocator UserEmail => Page.Locator("[data-testid=logged-in-user-email]");

    // <inheritdoc/>
    public async Task OpenBasket()
    {
        await BasketButton.ClickAsync();
    }

    // <inheritdoc/>
    public async Task OpenLogin()
    {
        await LoginButton.ClickAsync();
    }

    // <inheritdoc/>
    public async Task UserEmailShouldBe(string email)
    {
        await Expect(UserEmail).ToHaveTextAsync(email);
    }

}