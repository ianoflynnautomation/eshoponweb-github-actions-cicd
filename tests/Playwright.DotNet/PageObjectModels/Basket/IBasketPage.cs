
namespace Playwright.DotNet.PageObjectModels.Basket;

public interface IBasketPage
{
    Task<IBasketPage> Checkout();
    Task<IBasketPage> Update();
    Task<IBasketPage> ContinueShopping();
    
}