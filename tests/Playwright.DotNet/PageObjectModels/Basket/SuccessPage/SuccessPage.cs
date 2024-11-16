
using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket.SuccessPage;

public class SuccessPage(IPage page) : WebPage(page),  ISuccessPage
{
    private ILocator OrderCompleteMessage => Page.Locator("h1");
    private ILocator ContinueShoppingAnchor => Page.Locator("[data-testid=success-continue-shopping-anchor]");

    /// <inheritdoc/>
    public async Task<ISuccessPage> SuccessMessageShouldBe(string message)
    {
        await Assertions.Expect(OrderCompleteMessage).ToHaveTextAsync(message);

        return this;
    }

    /// <inheritdoc/>
    public async Task<ISuccessPage> ContinueShopping()
    {
        await ContinueShoppingAnchor.ClickAsync();

        return this;
    }
}
