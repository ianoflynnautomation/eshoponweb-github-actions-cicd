using System.IO;
using Microsoft.Playwright;
using Playwright.DotNet.Infra.NUnit;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]
[assembly: LevelOfParallelism(1)]

[TestFixture]
public class CustomerOrderSystemTests : InMemoryBaseTest
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
    public async Task TC_01_Customer_Order_UserJourney()
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
