
using Microsoft.Playwright;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Playwright.DotNet.Infra.XUnit;

public class BrowserTestFixture : IAsyncLifetime
{
    public IPage Page { get; private set; } = null!;
    public IBrowserContext Context { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    public IPlaywright Playwright { get; private set; } = null!;
    public TestOutputHelper Output { get; private set; } = null!;

    public virtual BrowserNewContextOptions ContextOptions()
    {
        return new()
        {
            Locale = "en-US",
            ColorScheme = ColorScheme.Light,
            RecordVideoDir = ".videos"
        };
    }

    protected virtual BrowserTypeLaunchOptions LaunchOptions { get; } = new BrowserTypeLaunchOptions()
    {
        Headless = true
    };

    public async Task InitializeAsync(TestOutputHelper output)
    {

        Output = output;

        Playwright ??= (await Microsoft.Playwright.Playwright.CreateAsync());

        Browser = await Playwright.Chromium.LaunchAsync(LaunchOptions);

        Context = await Browser.NewContextAsync(ContextOptions());

        await Context.Tracing.StartAsync(new()
        {
            Title = $"{nameof(ITestMethod)}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        Page = await Context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {

        //     var screenshotPath = Path.Combine(
        //         Environment.CurrentDirectory,
        //         "screenshots",
        //         $"{nameof(ITestMethod)}.png");

        // await Page.ScreenshotAsync(screenshotPath);
        // Output.WriteLine($"Screenshot: {screenshotPath}");

        await Context.CloseAsync();
        await Browser.CloseAsync();
        Playwright.Dispose();

        }

    // Provides a new page for each test.
    public async Task<IPage> CreateNewPageAsync()
    {
        return await Browser.NewPageAsync();
    }

    public async Task<IPage> CreateNewPageAsyncWithContext()
    {
        return await Context.NewPageAsync();
    }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}

