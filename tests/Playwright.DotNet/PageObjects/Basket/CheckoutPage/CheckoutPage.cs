using Playwright.DotNet.Components;
using Playwright.DotNet.Find;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.PageObjects.Basket.CheckoutPage;

/// <summary>
/// Represents the checkout page of the eShopOnWeb application.
/// </summary>
public class CheckoutPage : WebPage, ICheckoutPage
{
    private Button PayNowButton => App.Component.CreateByDataTestId<Button>("pay-now-button").ToBeClickable();
    private Button BackButton => App.Component.CreateByDataTestId<Button>("back-button").ToBeClickable();

    public override string Url => throw new NotImplementedException();

    /// <inheritdoc/>
    public ICheckoutPage BackToBasket()
    {
        BackButton.Click();

        return this;
    }

    /// <inheritdoc/>
    public ICheckoutPage PayNow()
    {
        PayNowButton.Click();

        return this;
    }

}