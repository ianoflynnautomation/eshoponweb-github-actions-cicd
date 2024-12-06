using Playwright.DotNet.Fixtures.XUnit;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;
using Xunit;

namespace EShopOnWeb.XUnit.InProcess.SystemTests;

[Collection(nameof(SystemTestCollection))]
public class CustomerOrderSystemTests(SystemTestFixtureXUnit fixture)
: XUnitInProcessSystemTestsBase(fixture)
    {

    private HeaderSection _headerSection;
    private LoginPage _loginPage;
    private HomePage _homePage;
    private BasketPage _basketPage;
    private CheckoutPage _checkoutPage;
    private SuccessPage _successPage;

    public override async Task InitializeAsync()
    {
       await base.InitializeAsync();
        _headerSection = new HeaderSection(Page);
        _loginPage = new LoginPage(Page);
        _homePage = new HomePage(Page);
        _basketPage = new BasketPage(Page);
        _checkoutPage = new CheckoutPage(Page);
        _successPage = new SuccessPage(Page);
    }

    [Fact]
    public async Task Customer_Order_UserJourney()
    {
        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);
        await _headerSection.OpenLogin();
        await _loginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await _homePage.FilterForProduct(".NET", "Mug");
        await _homePage.AddItemToBasket(".NET Black & White Mug");
        await _basketPage.Checkout();
        await _checkoutPage.PayNow();
        await _successPage.SuccessMessageShouldBe("Thanks for your Order!");
    }
}
