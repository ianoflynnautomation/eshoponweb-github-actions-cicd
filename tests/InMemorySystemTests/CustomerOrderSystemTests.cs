using System.IO;
using Microsoft.Playwright;
using Playwright.DotNet.PageObjectModels.Basket;
using Playwright.DotNet.PageObjectModels.Basket.CheckoutPage;
using Playwright.DotNet.PageObjectModels.Basket.SuccessPage;
using Playwright.DotNet.PageObjectModels.HomePage;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]

[TestFixture]
public class CustomerOrderSystemTests : BaseTest
{
    [Test]
    public async Task Customer_Order_UserJourney()
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

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            Locale = "en-US",
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "playwright-videos/"),
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
