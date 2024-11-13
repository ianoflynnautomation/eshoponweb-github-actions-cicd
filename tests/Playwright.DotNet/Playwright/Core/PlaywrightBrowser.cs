using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Core;

/// <summary>
/// Synchronous wrapper for Playwright Browser.
/// </summary>
public class PlaywrightBrowser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaywrightBrowser"/> class.
    /// </summary>
    /// <param name="browserType"></param>
    /// <param name="browser"></param>
    internal PlaywrightBrowser(BrowserType browserType, IBrowser browser)
    {
        BrowserType = browserType;
        WrappedBrowser = browser;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="browserType"></param>
    /// <param name="browser"></param>
    internal PlaywrightBrowser(IBrowserType browserType, IBrowser browser)
    {
        BrowserType = new BrowserType(browserType);
        WrappedBrowser = browser;
    }

    internal PlaywrightBrowser(IBrowser browser)
    {
        WrappedBrowser = browser;
    }

    public IBrowser WrappedBrowser { get; internal init; }

    public BrowserType? BrowserType { get; internal init; }

    internal List<BrowserContext> BrowserContexts { get; private init; } = [];

    public IReadOnlyList<BrowserContext> Contexts => BrowserContexts.AsReadOnly();

    public bool IsConnected => WrappedBrowser.IsConnected;

    public string Version => WrappedBrowser.Version;

    public event EventHandler<IBrowser> OnDisconnected
    {
        add => WrappedBrowser.Disconnected += value;
        remove => WrappedBrowser.Disconnected -= value;
    }

    public BrowserContext NewContext(BrowserNewContextOptions? options = null)
    {
        var newContext = new BrowserContext(this, WrappedBrowser.NewContextAsync(options).Result);
        BrowserContexts.Add(newContext);

        return newContext;
    }

    public BrowserPage NewPage(BrowserNewPageOptions? options = null)
    {
        var newPage = WrappedBrowser.NewPageAsync(options).Result;
        var browserContext = BrowserContexts.Find(x => x.WrappedBrowserContext.Equals(newPage.Context));

        if (browserContext == null)
        {
            browserContext = new BrowserContext(this, newPage.Context);
            BrowserContexts.Add(browserContext);
        }

        var newBrowserPage = new BrowserPage(newPage);

        browserContext.BrowserPages.Add(newBrowserPage);

        return newBrowserPage;
    }
}
