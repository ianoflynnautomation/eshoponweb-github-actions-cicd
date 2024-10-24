
namespace Playwright.DotNet.PageObjects.Basket;

/// <summary>
/// Represents the basket page of the eShopOnWeb application.
/// </summary>
public interface IBasketPage
{
    /// <summary>
    /// Updates the quantity of an item in the basket.
    /// </summary>
    /// <param name="item">Item in the basket to update</param>
    /// <param name="quantity">Quanity amount</param>
    IBasketPage UpdateQuantity(string item, int quantity);

    /// <summary>
    /// Clicks the checkout button to proceed to the checkout page.
    /// </summary>
    IBasketPage Checkout();

    /// <summary>
    /// Updates the basket.
    /// </summary>
    IBasketPage Update();

    /// <summary>
    /// Clicks the continue shopping button to return to the home page.
    /// </summary>
    IBasketPage ContinueShopping();
    
}