

using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;

public class CheckoutPage(IPage page) : WebPage(page), ICheckoutPage
{
    private ILocator PayNowButton => Page.Locator("[data-testid=pay-now-button]");
    private ILocator BackButton => Page.Locator("[data-testid=back-button]");

    /// <inheritdoc/>
    public async Task<ICheckoutPage> BackToBasket()
    {
        await BackButton.ClickAsync();

        return this;
    }

    /// <inheritdoc/>
    public async Task<ICheckoutPage> PayNow()
    {
       await PayNowButton.ClickAsync();

        return this;
    }

}