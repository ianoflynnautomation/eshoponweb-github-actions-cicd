

using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;

public class CheckoutPage(IPage page) : WebPage(page), ICheckoutPage
{
    private ILocator PayNowButton => Page.Locator("[data-testid=pay-now-button]");
    private ILocator BackButton => Page.Locator("[data-testid=back-button]");

    /// <inheritdoc/>
    public async Task BackToBasket()
    {
        await BackButton.ClickAsync();

        
    }

    /// <inheritdoc/>
    public async Task PayNow()
    {
       await PayNowButton.ClickAsync();

        
    }

}