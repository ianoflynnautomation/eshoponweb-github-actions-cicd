namespace Playwright.DotNet.PageObjectModels.Basket.SuccessPage;

public interface ISuccessPage
{
    Task SuccessMessageShouldBe(string message);
    
    Task ContinueShopping();

}
