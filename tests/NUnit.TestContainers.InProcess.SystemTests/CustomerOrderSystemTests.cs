
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.NUnit.TestContainers.InProcess.SystemTests;

[Parallelizable(ParallelScope.Self)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : TestContainersTestsBase
{
    private HeaderSection _headerSection;
    private LoginPage _loginPage;
    private HomePage _homePage;
    private BasketPage _basketPage;
    private CheckoutPage _checkoutPage;
    private SuccessPage _successPage;


    [SetUp]
    public async Task SetUp()
    {
        _headerSection = new HeaderSection(Page);
        _loginPage = new LoginPage(Page);
        _homePage = new HomePage(Page);
        _basketPage = new BasketPage(Page);
        _checkoutPage = new CheckoutPage(Page);
        _successPage = new SuccessPage(Page);

        await Page.GotoAsync(_fixture.ServerAddress);
    }

    [Test]
    public async Task Customer_Order_UserJourney()
    {
        await _headerSection.OpenLogin();
        await _loginPage.Login("demouser@microsoft.com", "Pass@word1", false);
        await _homePage.FilterForProduct(".NET", "Mug");
        await _homePage.AddItemToBasket(".NET Black & White Mug");
        await _basketPage.Checkout();
        await _checkoutPage.PayNow();
        await _successPage.SuccessMessageShouldBe("Thanks for your Order!");
    }

}
