
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
    public IHomePage FilterForProduct(string brand, string type)
    {
        if (brand is not null)
        {
            FilterBrand(brand);
        }

        if (type is not null)
        {
            FilterType(type);
        }

        Filter();

        return this;
    }

    /// <inheritdoc/>
    public IHomePage AddItemToBasket(string itemName)
    {
        AddToBasket(itemName).Click();

        return this;
    }

    /// <inheritdoc/>
    public IHomePage FilterBrand(string brand)
    {
        BrandFilter.SelectByText(brand);

        return this;
    }

    /// <inheritdoc/>
    public IHomePage FilterType(string type)
    {
        TypeFilter.SelectByText(type);

        return this;
    }

    /// <inheritdoc/>
    public IHomePage Filter()
    {
        FilterButton.Click();

        return this;
    }

}