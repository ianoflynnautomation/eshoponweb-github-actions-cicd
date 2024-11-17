
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Playwright.DotNet.Infra.NUnit;

/// <summary>
/// Each test will get a browser and can create as many contexts as it likes. 
/// Each test is responsible for cleaning up all the contexts it created.
/// </summary>
public class BrowserTestBase : BrowserTest
{

    // Declare the Context and Page
    public IPage Page { get; private set; } = null!;
    public IBrowserContext Context { get; private set; } = null!;
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
    public async Task OneTimeSetUp()
    {
        await Task.CompletedTask;
    }

    [SetUp]
    public async Task SetUp()
    {
        // Create Context
        Context = await Browser.NewContextAsync(ContextOptions());

        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        // Create a new page
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
        //TestContext.AddTestAttachment(tracePath, description: "Trace");


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
            //TestContext.AddTestAttachment(screenshotPath, description: "Screenshot");
        }

        //await Page.CloseAsync();
        await Context.CloseAsync();

        var videoPath = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-videos",
            $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.webm");
        if (Page.Video != null)
        {
            await Page.Video.SaveAsAsync(videoPath);
            //TestContext.AddTestAttachment(videoPath, description: "Video");
        }

        //await Browser.CloseAsync();
        //await Browser.DisposeAsync();


    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        //Playwright.Dispose();

    }
}
