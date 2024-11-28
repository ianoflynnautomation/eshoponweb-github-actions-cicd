using Microsoft.Playwright;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.DockerSystemTests;

[Parallelizable(ParallelScope.Self)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : DockerSystemTestsBase
{
    private IHeaderSection HeaderSection;
    private ILoginPage LoginPage;
    private IHomePage HomePage;
    private IBasketPage BasketPage;
    private ICheckoutPage CheckoutPage;
    private ISuccessPage SuccessPage;

    [SetUp]
    public async Task SetUp()
    {
        HeaderSection = new HeaderSection(Page);
        LoginPage = new LoginPage(Page);
        HomePage = new HomePage(Page);
        BasketPage = new BasketPage(Page);
        CheckoutPage = new CheckoutPage(Page);
        SuccessPage = new SuccessPage(Page);

        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);
    }

    [Test]
    public async Task Customer_Order_UserJourney()
    {
        await HeaderSection.OpenLogin();
        await LoginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await HomePage.FilterForProduct(".NET", "Mug");
        await HomePage.AddItemToBasket(".NET Black & White Mug");
        await BasketPage.Checkout();
        await CheckoutPage.PayNow();
        await SuccessPage.SuccessMessageShouldBe("Thanks for your Order!");
    }


    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {

            IgnoreHTTPSErrors = true,
            Locale = "en-US",
            BaseURL = _fixture.SystemTestHost.WebServerUrl
        };

    }
}
