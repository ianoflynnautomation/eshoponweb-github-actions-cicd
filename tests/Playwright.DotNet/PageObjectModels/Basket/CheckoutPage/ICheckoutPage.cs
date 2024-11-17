namespace Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;

public interface ICheckoutPage
{
    Task PayNow();
    Task BackToBasket();

}