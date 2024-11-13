using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.Playwright.Core;

/// <summary>
/// Synchronous wrapper of Playwright IBrowserContext.
/// </summary>
public partial class BrowserContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserContext"/> class.
    /// </summary>
    internal BrowserContext(PlaywrightBrowser browser, IBrowserContext context)
    {
        Browser = browser;
        WrappedBrowserContext = context;
    }

    internal BrowserContext(IBrowserContext context)
    {
        WrappedBrowserContext = context;
    }

    public IBrowserContext WrappedBrowserContext { get; internal init; }

    public PlaywrightBrowser? Browser { get; internal init; }

    internal List<BrowserPage> BrowserPages { get; private init; } = [];

    public IReadOnlyList<BrowserPage> Pages => BrowserPages.AsReadOnly();

    public IAPIRequestContext APIRequest => WrappedBrowserContext.APIRequest;

    public ITracing Tracing => WrappedBrowserContext.Tracing;
    
    public BrowserPage NewPage()
    {
        var newPage = new BrowserPage(this, WrappedBrowserContext.NewPageAsync().SyncResult());
        BrowserPages.Add(newPage);

        return newPage;
    }

}
