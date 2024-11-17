
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
        var headerSection = new HeaderSection(await Browser.NewPageAsync());
        await headerSection.OpenLogin();
        var loginPage = new LoginPage(await Browser.NewPageAsync());
        await loginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        var homepage = new HomePage(await Browser.NewPageAsync());
        await homepage.FilterForProduct(".NET", "Mug");
        await homepage.AddItemToBasket(".NET Black & White Mug");
        var basketPage = new BasketPage(await Browser.NewPageAsync());
        await basketPage.Checkout();
        var checkoutPage = new CheckoutPage(await Browser.NewPageAsync());
        await checkoutPage.PayNow();
        var successPage = new SuccessPage(await Browser.NewPageAsync());
        await successPage.SuccessMessageShouldBe("Thanks for your Order!");

    }

}
