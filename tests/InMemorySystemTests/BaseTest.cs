
using System;
using System.IO;
using NUnit.Framework.Interfaces;
using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.InMemorySystemTests;
/// <summary>
/// Each test gets a fresh copy of a web Page created in its own unique BrowserContext. 
/// Extending this class is the simplest way of writing a fully-functional Playwright test.
/// Note: You can override the ContextOptions method in each test file to control context options, 
/// the ones typically passed into the Browser.NewContextAsync() method. 
/// That way you can specify all kinds of emulation options for your test file individually.
/// </summary>
public class BaseTest : PageTest
{
    protected SystemTestFixture _fixture;
    protected TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _fixture = new SystemTestFixture();
    }

    [SetUp]
    public async Task SetUp()
    {

        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await Page.GotoAsync(_fixture.SystemTestHost.WebServerUrl);

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
        //await Browser.CloseAsync();
        //await Browser.DisposeAsync();

        var videoPath = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-videos",
            $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.webm");
        if (Page.Video != null)
        {
            await Page.Video.SaveAsAsync(videoPath);
            //TestContext.AddTestAttachment(videoPath, description: "Video");
        }


    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        //Playwright.Dispose();
        _fixture.Dispose();

    }
}
