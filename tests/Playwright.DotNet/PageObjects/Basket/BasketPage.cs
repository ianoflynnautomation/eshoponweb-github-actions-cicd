using System.Reflection.Metadata.Ecma335;
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
    {
        await UpdateButton.ClickAsync();
        return this;
    }

    /// <inheritdoc/>
    public async Task<IBasketPage> UpdateQuantity(string item, int quantity)
    {

        return this;
    }

}

