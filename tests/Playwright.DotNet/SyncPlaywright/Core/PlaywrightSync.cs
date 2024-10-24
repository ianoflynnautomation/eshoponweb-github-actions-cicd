using Microsoft.Playwright;

namespace Playwright.DotNet.SyncPlaywright.Core;

/// <summary>
/// Synchronous wrapper for Playwright.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlaywrightSync"/> class.
/// </remarks>
/// <param name="playwright"></param>
public class PlaywrightSync(IPlaywright playwright)
{
    /// <summary>
    /// Gets the wrapped playwright.
    /// </summary>
    public IPlaywright WrappedPlaywright { get; internal init; } = playwright;

    /// <summary>
    /// Gets the browsers.
    /// </summary>
    /// <param name="browserType">Type of browser</param>
    /// <returns></returns>

    public BrowserType this[string browserType] => new BrowserType(WrappedPlaywright[browserType]);

    /// <summary>
    /// Gets the chromium browser.
    /// </summary>
    public BrowserType Chromium => new BrowserType(WrappedPlaywright.Chromium);

    /// <summary>
    /// Gets the devices.
    /// </summary>
    public IReadOnlyDictionary<string, BrowserNewContextOptions> Devices => WrappedPlaywright.Devices;

    /// <summary>
    /// Gets the firefox browser.
    /// </summary>
    public BrowserType Firefox => new BrowserType(WrappedPlaywright.Firefox);

    /// <summary>
    /// Gets the api request.
    /// </summary>
    public IAPIRequest APIRequest => WrappedPlaywright.APIRequest;

    /// <summary>
    /// Gets the selectors.
    /// </summary>
    public ISelectors Selectors => WrappedPlaywright.Selectors;

    /// <summary>
    ///  Gets the webkit browser.
    /// </summary>
    public BrowserType Webkit => new BrowserType(WrappedPlaywright.Webkit);

    /// <summary>
    /// Diposes all browsers.
    /// </summary>
    public void Dispose()
    {
        WrappedPlaywright.Dispose();
    }
}
