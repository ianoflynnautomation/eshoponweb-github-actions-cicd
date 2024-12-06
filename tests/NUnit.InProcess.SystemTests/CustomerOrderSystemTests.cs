using System.IO;
using Microsoft.Playwright;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.NUnit.InProcess.SystemTests;
[Parallelizable(ParallelScope.Self)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : NUnitInProcessSystemTestsBase
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

        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);
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

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            Locale = "en-US",
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "playwright-videos"),
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
        };
    }

    // public override BrowserNewContextOptions ContextOptions()
    // {
    //     return new BrowserNewContextOptions
    //     {
    //         AcceptDownloads = true,
    //         BaseURL = null,
    //         BypassCSP = null,
    //         ColorScheme = ColorScheme.Dark,
    //         DeviceScaleFactor = 1,
    //         ExtraHTTPHeaders = null,
    //         Geolocation = null,
    //         HasTouch = false,
    //         HttpCredentials = null,
    //         IgnoreHTTPSErrors = false,
    //         IsMobile = false,
    //         JavaScriptEnabled = true,
    //         Locale = "en-US",
    //         Offline = false,
    //         Permissions = null,
    //         Proxy = null,
    //         RecordHarContent = null,
    //         RecordHarMode = null,
    //         RecordHarOmitContent = null,
    //         RecordHarPath = null,
    //         RecordHarUrlFilterString = null,
    //         RecordHarUrlFilterRegex = null,
    //         RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "recordings"),
    //         RecordVideoSize = null,
    //         ReducedMotion = null,
    //         ScreenSize = null,
    //         ServiceWorkers = null,
    //         StorageState = null,
    //         StorageStatePath = null,
    //         StrictSelectors = null,
    //         TimezoneId = null,
    //         UserAgent = null,
    //         ViewportSize = new ViewportSize
    //         {
    //             Width = 1920,
    //             Height = 1080
    //         },
    //     };
    // }

}
