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

    private IHeaderSection HeaderSection;
    private ILoginPage LoginPage;
    private IHomePage HomePage;
    private IBasketPage BasketPage;
    private ICheckoutPage CheckoutPage;
    private ISuccessPage SuccessPage;

    public override async Task InitializeAsync()
    {
       await base.InitializeAsync();
        HeaderSection = new HeaderSection(Page);
        LoginPage = new LoginPage(Page);
        HomePage = new HomePage(Page);
        BasketPage = new BasketPage(Page);
        CheckoutPage = new CheckoutPage(Page);
        SuccessPage = new SuccessPage(Page);
    }

    [Fact]
    public async Task Customer_Order_UserJourney()
    {
        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);
        await HeaderSection.OpenLogin();
        await LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await HomePage.FilterForProduct(".NET", "Mug");
        await HomePage.AddItemToBasket(".NET Black & White Mug");
        await BasketPage.Checkout();
        await CheckoutPage.PayNow();
        await SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }
}
