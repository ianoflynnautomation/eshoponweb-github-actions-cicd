
using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.PageObjects.HomePage;

/// <summary>
/// Represents the home page of the eShopOnWeb application.
/// </summary>
public class HomePage : WebPage, IHomePage
{
    private Button AddToBasket(string item) => App.Component.CreateByDataTestId<Button>($"add-to-basket-button-{item}").ToBeClickable();
    private Button FilterButton => App.Component.CreateByDataTestId<Button>("catalog-filter-button").ToBeClickable();
    private Select BrandFilter => App.Component.CreateByDataTestId<Select>($"catalog-brand-filter").ToBeVisible();
    private Select TypeFilter => App.Component.CreateByDataTestId<Select>($"catalog-type-filter").ToBeVisible();

    public override string Url => throw new NotImplementedException();

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
       await BrandFilter.SelectByTextAsync(brand);

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> FilterType(string type)
    {
       await TypeFilter.SelectByTextAsync(type);

        return this;
    }

    /// <inheritdoc/>
    public async Task<IHomePage> Filter()
    {
        await FilterButton.ClickAsync();

        return this;
    }

}