
namespace Playwright.DotNet.PageObjectModels.HomePage;

public interface IHomePage
{
    Task<IHomePage> FilterBrand(string brand);
    Task<IHomePage> FilterType(string type);
    Task<IHomePage> Filter();
    Task<IHomePage> FilterForProduct(string brand, string type);
    Task<IHomePage> AddItemToBasket(string itemName);

}