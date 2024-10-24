using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.PageObjects.Basket;

/// <summary>
/// Represents the basket page of the eShopOnWeb application.
/// </summary>
public class BasketPage : WebPage, IBasketPage
{
    private Button CheckoutButton => App.Component.CreateByDataTestId<Button>("checkout-button").ToBeClickable();
    private Button UpdateButton => App.Component.CreateByDataTestId<Button>("update-button").ToBeClickable();
    private Button ContinueShoppingButton => App.Component.CreateByDataTestId<Button>("continue-shopping-button").ToBeClickable();
    public override string Url => throw new NotImplementedException();

    /// <inheritdoc/>
    public IBasketPage Checkout()
    {
        CheckoutButton.Click();
        return this;
    }

    /// <inheritdoc/>
    public IBasketPage ContinueShopping()
    {
        ContinueShoppingButton.Click();
        return this;
    }

    /// <inheritdoc/>
    public IBasketPage Update()
    {
        UpdateButton.Click();
        return this;
    }

    /// <inheritdoc/>
    public IBasketPage UpdateQuantity(string item, int quantity)
    {
        throw new NotImplementedException();
    }

}

