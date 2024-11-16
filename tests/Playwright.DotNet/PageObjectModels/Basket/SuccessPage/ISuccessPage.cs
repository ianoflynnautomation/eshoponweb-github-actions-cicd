namespace Playwright.DotNet.PageObjectModels.Basket.SuccessPage;

public interface ISuccessPage
{
    Task<ISuccessPage> SuccessMessageShouldBe(string message);
    Task<ISuccessPage> ContinueShopping();

}
