
using Microsoft.Playwright;
using Playwright.DotNet.Fixtures;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Playwright.DotNet.Infra.XUnit;

public class BrowserTestFixture : IAsyncLifetime
{

    public IPage Page { get; set; } = null!;
    public IBrowserContext Context { get; set; } = null!;
    public IBrowser Browser { get; set; } = null!;
    private IPlaywright Playwright { get;  set; } = null!;

    // public ITestOutputHelper? _testOutputHelper = testOutputHelper;

    public virtual BrowserNewContextOptions ContextOptions()
    {
        return new()
        {
            Locale = "en-US",
            ColorScheme = ColorScheme.Light,
            RecordVideoDir = ".videos"
        };
    }

    protected virtual BrowserTypeLaunchOptions LaunchOptions { get; } = new()
    {
        Headless = false,
        Channel = "msedge",
        Timeout = 30000,
        SlowMo = 100

    };

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(LaunchOptions);

        Context = await Browser.NewContextAsync(ContextOptions());

        await Context.Tracing.StartAsync(new()
        {
            Title = Guid.NewGuid().ToString(),
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        Page = await Context.NewPageAsync();
        // Capture output from the browser to the test logs
        // Page.Console += (_, e) => _testOutputHelper.WriteLine(e.Text);
        // Page.PageError += (_, e) => _testOutputHelper.WriteLine(e);
    }

    public async Task DisposeAsync()
    {

        var tracePath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "playwright-traces",
               $"{Guid.NewGuid}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.zip");

        await Context.Tracing.StopAsync(new()
        {
            Path = tracePath
        });



        var screenshotPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "playwright-screenshot",
            $"{Guid.NewGuid}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png");

        await Page.ScreenshotAsync(new()
        {
            Path = screenshotPath,
        });


        await Context.CloseAsync();

        var videoPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "playwright-videos",
            $"{Guid.NewGuid}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.webm");
        if (Page.Video != null)
        {
            await Page.Video.SaveAsAsync(videoPath);
        }

        await Browser.DisposeAsync();
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

//    public Task InitializeAsync()
//     {
//        var exitCode = Microsoft.Playwright.Program.Main(new[] {"install"});

//         if (exitCode != 0)
//         {
//             throw new InvalidOperationException($"Playwright exited with code {exitCode}.");
//         }

//         return Task.CompletedTask;
//     }


    // public ITestOutputHelper? TestOutputHelper
    // {
    //     get => _testOutputHelper;
    //     set
    //     {
    //         _testOutputHelper = (TestOutputHelper?)value;

    //     }
    // }

}

