
using Microsoft.Playwright;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.TestContainersSystemTests;

[Parallelizable(ParallelScope.Self)]
//[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[assembly: LevelOfParallelism(2)]

[TestFixture]
public class CustomerOrderSystemTests : BaseTest
{


    [Test]
    public async Task TC_01_Customer_Order_UserJourney()
    {
        await using var browser = await Playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
         var page = await context.NewPageAsync();
        await page.GotoAsync(_fixture.ServerAddress);

        var homepage = new HomePage(page);
        var headerSection = new HeaderSection(page);
        var loginPage = new LoginPage(page);
        var basketPage = new BasketPage(page);
        var checkoutPage = new CheckoutPage(page);
        var successPage = new SuccessPage(page);

        await headerSection.OpenLogin();
        await loginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await homepage.FilterForProduct(".NET", "Mug");
        await homepage.AddItemToBasket(".NET Black & White Mug");
        await basketPage.Checkout();
        await checkoutPage.PayNow();
        await successPage.SuccessMessageShouldBe("Thanks for your Order!");
   
    }

}
