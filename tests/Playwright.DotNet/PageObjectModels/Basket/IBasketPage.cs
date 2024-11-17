
namespace Playwright.DotNet.PageObjectModels.Basket;

public interface IBasketPage
{
    Task Checkout();
    Task Update();
    Task ContinueShopping();
    
}