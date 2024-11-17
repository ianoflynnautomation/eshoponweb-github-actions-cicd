
namespace Playwright.DotNet.PageObjectModels.HomePage;

public interface IHomePage
{
    Task FilterBrand(string brand);
    Task FilterType(string type);
    Task Filter();
    Task FilterForProduct(string brand, string type);
    Task AddItemToBasket(string itemName);

}