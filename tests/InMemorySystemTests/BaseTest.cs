
using System;
using System.IO;
using NUnit.Framework.Interfaces;
using Playwright.DotNet.Fixtures;

namespace EShopOnWeb.InMemorySystemTests;

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


        await Context.Tracing.StopAsync(new()
        {
            Path = failed ? Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                $"{TestContext.CurrentContext.Test.Name}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.zip"
            ) : null,
        });


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
            TestContext.AddTestAttachment(videoPath, description: "Video");
        }


    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        //Playwright.Dispose();
        _fixture.Dispose();

    }
}
