using Playwright.DotNet.Playwright.Core;

namespace Playwright.DotNet.Services;

/// <summary>
/// Represents the wrapped browser.
/// </summary>
public class WebService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WrappedBrowser"/> class.
    /// </summary>
    internal WebService(WrappedBrowser wrappedBrowser)
    {
        WrappedBrowser = wrappedBrowser;
    }

    public WrappedBrowser WrappedBrowser { get; set; }
    public PlaywrightBrowser Browser => WrappedBrowser.Browser;
    public BrowserContext CurrentContext => WrappedBrowser.CurrentContext;
    public BrowserPage CurrentPage => WrappedBrowser.CurrentPage;
}