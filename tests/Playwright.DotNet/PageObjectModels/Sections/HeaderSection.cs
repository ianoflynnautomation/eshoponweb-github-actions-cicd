
using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Sections;

public class HeaderSection(IPage page) : WebPage(page), IHeaderSection
{
    private ILocator BasketButton => Page.Locator("img[src='/images/cart.png']");
    private ILocator LoginButton => Page.Locator("[data-testid=identity-login-button]");
    private ILocator UserEmail => Page.Locator("[data-testid=logged-in-user-email]");

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
    public async Task<IHeaderSection> UserEmailShouldBe(string email)
    {
        await Expect(UserEmail).ToHaveTextAsync(email);

        return this;
    }

}