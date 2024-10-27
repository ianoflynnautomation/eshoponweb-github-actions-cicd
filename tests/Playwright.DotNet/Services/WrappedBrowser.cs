using Playwright.DotNet.SyncPlaywright.Core;

namespace Playwright.DotNet.Services;

/// <summary>
/// Represents the wrapped browser.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WrappedBrowser"/> class.
/// </remarks>
public class WrappedBrowser
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public WrappedBrowser()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public WrappedBrowser(
        PlaywrightSync playwright,
        PlaywrightBrowser browser,
        BrowserContext context,
        BrowserPage page)
    {
        Playwright = playwright;
        Browser = browser;
        CurrentContext = context;
        CurrentPage = page;
    }

    public PlaywrightSync Playwright { get; internal set; }
    public PlaywrightBrowser Browser { get; internal set; }
    public BrowserContext CurrentContext { get; internal set; }
    public BrowserPage CurrentPage { get; internal set; }

    /// <summary>
    /// Quits the browser.
    /// </summary>
    public void Quit()
    {
        try
        {
            CurrentPage?.Close();
            CurrentContext?.Close();
            Browser?.Close();
            Playwright?.Dispose();
        }
        catch
        {
            // Ignore exceptions
        }
        // catch (Exception ex)
        // {
        //     ex.PrintStackTrace();
        // }
    }
}