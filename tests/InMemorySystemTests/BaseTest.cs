
using System;
using System.Data.Common;
using System.IO;
using System.Security;
using System.Security.Policy;
using Microsoft.Playwright;
using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.InMemorySystemTests;

public class BaseTest : PageTest
{
    protected IPage Page;
    protected SystemTestFixture _fixture;
    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public async Task SetUp()
    {
        var Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Args = null,
            Channel = null,
            ChromiumSandbox = null,
            Devtools = true,
            DownloadsPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "downloads"),
            Env = null,
            ExecutablePath = null,
            FirefoxUserPrefs = null,
            HandleSIGHUP = null,
            HandleSIGINT = null,
            HandleSIGTERM = null,
            Headless = true,
            IgnoreAllDefaultArgs = null,
            IgnoreDefaultArgs = null,
            Proxy = null,
            SlowMo = null,
            Timeout = 5000,
            TracesDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "playwright-traces")

        });


        var Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            AcceptDownloads = true,
            BaseURL = null,
            BypassCSP = null,
            ColorScheme = ColorScheme.Dark,
            DeviceScaleFactor = 1,
            ExtraHTTPHeaders = null,
            Geolocation = null,
            HasTouch = false,
            HttpCredentials = null,
            IgnoreHTTPSErrors = false,
            IsMobile = false,
            JavaScriptEnabled = true,
            Locale = "en-US",
            Offline = false,
            Permissions = null,
            Proxy = null,
            RecordHarContent = null,
            RecordHarMode = null,
            RecordHarOmitContent = null,
            RecordHarPath = null,
            RecordHarUrlFilterString = null,
            RecordHarUrlFilterRegex = null,
            RecordVideoDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "recordings"),
            RecordVideoSize = null,
            ReducedMotion = null,
            ScreenSize = null,
            ServiceWorkers = null,
            StorageState = null,
            StorageStatePath = null,
            StrictSelectors = null,
            TimezoneId = null,
            UserAgent = null,
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
        });

        Page = await Context.NewPageAsync();
        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);

        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true

        });

    }

    [TearDown]
    public async Task TearDown()
    {
        var failed = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

        if (failed)
        {
            //     await Page.ScreenshotAsync(new()
            // {
            //     Path = Path.Combine(
            //         TestContext.CurrentContext.WorkDirectory,
            //         "screenshots",
            //         $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.png"
            //     ),
            //     FullPage = true,
            // });
        }


        await Context.Tracing.StopAsync(new()
        {
            Path = failed ? Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
            ) : null,
        });
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture.Dispose();

    }
}
