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
    public WrappedBrowser()
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
            CurrentPage = null;

            CurrentContext?.Close();
            CurrentContext = null;

            Browser?.Close();
            Browser = null;

            Playwright?.Dispose();
            Playwright = null;
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