
using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket;

public class BasketPage(IPage page) : WebPage(page), IBasketPage
{
    private ILocator CheckoutButton => Page.Locator("[data-testid=checkout-button]");
    private ILocator UpdateButton => Page.Locator("[data-testid=update-button]");
    private ILocator ContinueShoppingButton => Page.Locator("[data-testid=continue-shopping-button]");
    

    /// <inheritdoc/>
    public async Task Checkout()
    {
        await CheckoutButton.ClickAsync();
    }

    /// <inheritdoc/>
    public async Task ContinueShopping()
    {
        await ContinueShoppingButton.ClickAsync();
    }

    /// <inheritdoc/>
    public async Task Update()
    {
        await UpdateButton.ClickAsync();
    }

}

