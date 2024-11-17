using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.DockerSystemTests;

[Parallelizable(ParallelScope.Self)]
//[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : BaseTest
{
    [Test]
    public async Task TC_01_Customer_Order_UserJourney()
    {
        var headerSection = new HeaderSection(Page);
        await headerSection.OpenLogin();

        var loginPage = new LoginPage(Page);
        await loginPage.Login("demouser@microsoft.com", "Pass@word1", false);

        var homepage = new HomePage(Page);
        await homepage.FilterForProduct(".NET", "Mug");
        await homepage.AddItemToBasket(".NET Black & White Mug");

        var basketPage = new BasketPage(Page);
        await basketPage.Checkout();

        var checkoutPage = new CheckoutPage(Page);
        await checkoutPage.PayNow();
        
        var successPage = new SuccessPage(Page);
        await successPage.SuccessMessageShouldBe("Thanks for your Order!");

    }

}
