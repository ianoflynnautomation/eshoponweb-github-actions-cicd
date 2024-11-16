namespace Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;

public interface ICheckoutPage
{
    Task<ICheckoutPage> PayNow();
    Task<ICheckoutPage> BackToBasket();

}