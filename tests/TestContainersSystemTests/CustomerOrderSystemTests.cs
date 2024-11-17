
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.TestContainersSystemTests;

[Parallelizable(ParallelScope.Self)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : DockerContainersBaseTest
{

    [Test]
    public async Task TC_01_Customer_Order_UserJourney()
    {
        var homepage = new HomePage(Page);
        var headerSection = new HeaderSection(Page);
        var loginPage = new LoginPage(Page);
        var basketPage = new BasketPage(Page);
        var checkoutPage = new CheckoutPage(Page);
        var successPage = new SuccessPage(Page);

        await headerSection.OpenLogin();
        await loginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await homepage.FilterForProduct(".NET", "Mug");
        await homepage.AddItemToBasket(".NET Black & White Mug");
        await basketPage.Checkout();
        await checkoutPage.PayNow();
        await successPage.SuccessMessageShouldBe("Thanks for your Order!");
   
    }

}
