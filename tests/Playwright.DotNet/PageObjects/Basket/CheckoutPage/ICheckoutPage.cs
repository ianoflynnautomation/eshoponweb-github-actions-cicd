namespace Playwright.DotNet.PageObjects.Basket.CheckoutPage;

/// <summary>
/// Represents the checkout page of the eShopOnWeb application.
/// </summary>
public interface ICheckoutPage
{
    /// <summary>
    /// Clicks the pay now button.
    /// </summary>
    Task<ICheckoutPage> PayNow();

    /// <summary>
    /// Clicks the back button to return to the basket.
    /// </summary>
    Task<ICheckoutPage> BackToBasket();

}