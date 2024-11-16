using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.HomePage;

public class HomePage(IPage page) : WebPage(page), IHomePage
{
    private ILocator BrandFilter => Page.Locator("[data-testid=catalog-brand-filter]");
    private ILocator TypeFilter => Page.Locator("[data-testid=catalog-type-filter]");
    private ILocator FilterButton => Page.Locator("[data-testid=catalog-filter-button]");
    private ILocator AddToBasket(string item) => Page.Locator($"data-testid=add-to-basket-button-{item}");

    /// <inheritdoc/>
    public async Task<IHomePage> FilterForProduct(string brand, string type)
    {
        if (brand is not null)
        {
            await FilterBrand(brand);
        }

        if (type is not null)
        {
            await FilterType(type);
        }

        await Filter();

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> AddItemToBasket(string itemName)
    {
        await AddToBasket(itemName).ClickAsync();

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> FilterBrand(string brand)
    {
        await BrandFilter.SelectOptionAsync(new SelectOptionValue() { Label = brand });

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> FilterType(string type)
    {
        await TypeFilter.SelectOptionAsync(new SelectOptionValue() { Label = type });

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> Filter()
    {
        await FilterButton.ClickAsync(new(){ Force = true });

        return this;
    }
}