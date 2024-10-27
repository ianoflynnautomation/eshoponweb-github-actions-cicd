using System.Diagnostics;
using Microsoft.Playwright;

namespace Playwright.DotNet.SyncPlaywright.Core;

/// <summary>
/// Synchronous wrapper for Playwright IBrowserType.
/// </summary>
public class BrowserType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserType"/> class.
    /// </summary>
    /// <param name="browserType"></param>
    internal BrowserType(IBrowserType browserType)
    {
        WrappedBrowserType = browserType;
    }

    /// <summary>
    /// Gets the wrapped browser type.
    /// </summary>
    public IBrowserType WrappedBrowserType { get; internal init; }

    /// <summary>
    /// Gets the executable path.
    /// </summary>
    public string ExecutablePath => WrappedBrowserType.ExecutablePath;

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name => WrappedBrowserType.Name;

    /// <summary>
    /// Connects to the browser.
    /// </summary>
    /// <param name="wsEndpoint"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public PlaywrightBrowser Connect(string wsEndpoint, BrowserTypeConnectOptions? options = null)
    {
        return new PlaywrightBrowser(this, WrappedBrowserType.ConnectAsync(wsEndpoint, options).Result);
    }

    /// <summary>
    /// Connects to the browser over CDP.
    /// </summary>
    /// <param name="endpointURL"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public PlaywrightBrowser ConnectOverCDP(string endpointURL, BrowserTypeConnectOverCDPOptions? options = null)
    {
        return new PlaywrightBrowser(this, WrappedBrowserType.ConnectOverCDPAsync(endpointURL, options).Result);
    }

    /// <summary>
    /// Launches the browser.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public PlaywrightBrowser Launch(BrowserTypeLaunchOptions? options = null)
    {
        return new PlaywrightBrowser(this, WrappedBrowserType.LaunchAsync(options).Result);
    }

    /// <summary>
    /// Launches the persistent context.
    /// </summary>
    /// <param name="userDataDir"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public BrowserContext LaunchPersistentContext(string userDataDir, BrowserTypeLaunchPersistentContextOptions? options = null)
    {
        var persistentContext = WrappedBrowserType.LaunchPersistentContextAsync(userDataDir, options).Result;

        return new BrowserContext(new PlaywrightBrowser(this, persistentContext.Browser), persistentContext);
    }
}
