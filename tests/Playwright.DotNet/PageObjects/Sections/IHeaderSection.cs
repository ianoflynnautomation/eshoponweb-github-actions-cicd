
namespace Playwright.DotNet.PageObjects.Sections;

/// <summary>
/// Represents the header section of the eShopOnWeb application.
/// </summary>
public interface IHeaderSection
{
    /// <summary>
    /// Opens the basket by clicking the basket icon.
    /// </summary>
    Task<IHeaderSection> OpenBasket();

    /// <summary>
    /// Opens the login page by clicking the login icon.
    /// </summary>
    Task<IHeaderSection> OpenLogin();

    /// <summary>
    /// Asserts that the user email is displayed in the header.
    /// </summary>
    /// <param name="email">Expected email address to be displayed</param>
    Task<IHeaderSection> UserEmailShouldBe(string email);
}