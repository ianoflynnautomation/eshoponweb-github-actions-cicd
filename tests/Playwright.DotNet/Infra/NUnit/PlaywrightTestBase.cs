using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Developer.MicrosoftPlaywrightTesting.TestLogger;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Microsoft.Playwright.TestAdapter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Playwright.DotNet.Infra.NUnit;

/// <summary>
/// Each test will get a browser and can create as many contexts as it likes.
/// Each test is responsible for cleaning up all the contexts it created.
/// </summary>

[TestFixture]
public class PlaywrightTestBase : PlaywrightTest
{
    // Declare Browser, Context and Page
    public IPage Page { get; private set; } = null!;
    public IBrowserContext Context { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;

    protected TestContext TestContext => TestContext.CurrentContext;

    public virtual BrowserNewContextOptions ContextOptions()
    {
        return new()
        {
            Locale = "en-US",
            ColorScheme = ColorScheme.Light,
            RecordVideoDir = ".videos"
        };
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    [SetUp]
    public async Task SetUp()
    {
        // Create Browser
        if (TestContext.Parameters.Get(RunSettingKey.UseCloudHostedBrowsers) == "false")
        {
            Browser = await BrowserType.LaunchAsync(PlaywrightSettingsProvider.LaunchOptions);
        }
        else
        {
            /* Connect Remote Browser using BrowserType.ConnectAsync
             * fetches service connect options like wsEndpoint and options
             * add x-playwright-launch-options header to pass launch options likes channel, headless, etc.
             */
            var playwrightService = new PlaywrightService();
            var connectOptions = await playwrightService.GetConnectOptionsAsync<BrowserTypeConnectOptions>();
            var launchOptionString = JsonSerializer.Serialize(PlaywrightSettingsProvider.LaunchOptions, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            if (connectOptions.Options!.Headers is not null)
            {
                connectOptions.Options.Headers = connectOptions.Options.Headers.Concat(new Dictionary<string, string> { { "x-playwright-launch-options", launchOptionString } });
            }
            else
            {
                connectOptions.Options.Headers = new Dictionary<string, string> { { "x-playwright-launch-options", launchOptionString } };
            }
            Browser = await BrowserType.ConnectAsync(connectOptions.WsEndpoint!, connectOptions.Options!);
        }

        // Create context and page
        Context = await Browser.NewContextAsync(ContextOptions());


        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true

        });

        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        var failed = TestContext.CurrentContext.Result.Outcome == ResultState.Error
            || TestContext.CurrentContext.Result.Outcome == ResultState.Failure;


        var tracePath = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.zip");

        await Context.Tracing.StopAsync(new()
        {
            Path = failed ? tracePath : null
        });
        TestContext.AddTestAttachment(tracePath, description: "Trace");


        // Take a screenshot on error and add it as an attachment
        if (TestContext.CurrentContext.Result.Outcome == ResultState.Error)
        {
            var screenshotPath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-screenshot",
                $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png");

            await Page.ScreenshotAsync(new()
            {
                Path = screenshotPath,
            });
            TestContext.AddTestAttachment(screenshotPath, description: "Screenshot");
        }

        await Context.CloseAsync();

        var videoPath = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-videos",
            $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.webm");
        if (Page.Video != null)
        {
            await Page.Video.SaveAsAsync(videoPath);
            TestContext.AddTestAttachment(videoPath, description: "Video");
        }

        await Browser.CloseAsync();

    }
}