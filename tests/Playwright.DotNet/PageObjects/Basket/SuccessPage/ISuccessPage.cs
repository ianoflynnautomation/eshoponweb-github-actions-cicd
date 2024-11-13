namespace Playwright.DotNet.PageObjects.Basket.SuccessPage;

/// <summary>
/// Represents the success page of the eShopOnWeb application.
/// </summary>
public interface ISuccessPage
{
    /// <summary>
    /// Asserts that the success message is displayed.
    /// </summary>
    /// <param name="message">Expected message to be displayed</param>
    ISuccessPage SuccessMessageShouldBe(string message);

    /// <summary>
    /// Clicks the continue shopping button to return to the home page.
    /// </summary>
    Task<ISuccessPage> ContinueShopping();

}
