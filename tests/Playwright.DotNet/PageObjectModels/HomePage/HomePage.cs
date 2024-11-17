using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels.HomePage;

public class HomePage(IPage page) : WebPage(page), IHomePage
{
    private ILocator BrandFilter => Page.Locator("[data-testid=catalog-brand-filter]");
    private ILocator TypeFilter => Page.Locator("[data-testid=catalog-type-filter]");
    private ILocator FilterButton => Page.Locator("[data-testid=catalog-filter-button]");
    private ILocator AddToBasket(string item) => Page.Locator($"data-testid=add-to-basket-button-{item}");

    /// <inheritdoc/>
    public async Task FilterForProduct(string brand, string type)
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
    }

    /// <inheritdoc/>
    public async Task AddItemToBasket(string itemName)
    {
        await AddToBasket(itemName).ClickAsync();
;
    }

    /// <inheritdoc/>
    public async Task FilterBrand(string brand)
    {
        await BrandFilter.SelectOptionAsync(new SelectOptionValue() { Label = brand });

    }

    /// <inheritdoc/>
    public async Task FilterType(string type)
    {
        await TypeFilter.SelectOptionAsync(new SelectOptionValue() { Label = type });
    }

    /// <inheritdoc/>
    public async Task Filter()
    {
        await FilterButton.ClickAsync(new(){ Force = true });

    }
}