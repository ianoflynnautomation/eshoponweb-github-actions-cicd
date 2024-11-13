
namespace Playwright.DotNet.PageObjects.HomePage;

/// <summary>
/// Represents the home page of the eShopOnWeb application.
/// </summary>
public interface IHomePage
{
    /// <summary>
    /// Selects the brand from the filter.
    /// </summary>
    /// <param name="brand">The brand to filter by</param>
    Task<IHomePage> FilterBrand(string brand);

    /// <summary>
    /// Selects the type of product from the filter.
    /// </summary>
    /// <param name="type">The type of product to filter bu</param>
    Task<IHomePage> FilterType(string type);

    /// <summary>
    /// Clicks the filter button.
    /// </summary>
    Task<IHomePage> Filter();

    /// <summary>
    /// Filters the products on the home page based on the brand and type.
    /// </summary>
    /// <param name="brand">The brand to filter by</param>
    /// <param name="type">The type of product to filter by</param>
    Task<IHomePage> FilterForProduct(string brand, string type);

    /// <summary>
    /// Adds an item to the basket.
    /// </summary>
    /// <param name="itemName">Name of item to add</param>
    Task<IHomePage> AddItemToBasket(string itemName);

}