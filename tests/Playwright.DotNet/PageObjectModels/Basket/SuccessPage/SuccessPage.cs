using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket.SuccessPage;

public class SuccessPage(IPage page) : WebPage(page),  ISuccessPage
{
    private ILocator OrderCompleteMessage => Page.Locator("h1");
    private ILocator ContinueShoppingAnchor => Page.Locator("[data-testid=success-continue-shopping-anchor]");

    /// <inheritdoc/>
    public async Task SuccessMessageShouldBe(string message)
    {
        await Expect(OrderCompleteMessage).ToHaveTextAsync(message);    
    }

    /// <inheritdoc/>
    public async Task ContinueShopping()
    {
        await ContinueShoppingAnchor.ClickAsync();
    }
    
}
