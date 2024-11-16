
using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.Basket;

public class BasketPage(IPage page) : WebPage(page), IBasketPage
{
    private ILocator CheckoutButton => Page.Locator("[data-testid=checkout-button]");
    private ILocator UpdateButton => Page.Locator("[data-testid=update-button]");
    private ILocator ContinueShoppingButton => Page.Locator("[data-testid=continue-shopping-button]");
    

    /// <inheritdoc/>
    public async Task<IBasketPage> Checkout()
    {
        await CheckoutButton.ClickAsync();
        return this;
    }

    /// <inheritdoc/>
    public async Task<IBasketPage> ContinueShopping()
    {
        await ContinueShoppingButton.ClickAsync();
        return this;
    }

    /// <inheritdoc/>
    public async Task<IBasketPage> Update()
    {;
        await UpdateButton.ClickAsync();
        return this;
    }

}

