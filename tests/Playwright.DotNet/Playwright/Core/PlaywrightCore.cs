using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Core;

/// <summary>
/// Synchronous wrapper for Playwright.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlaywrightCore"/> class.
/// </remarks>
/// <param name="playwright"></param>
public class PlaywrightCore(IPlaywright playwright)
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

    public BrowserType this[string browserType] => new(WrappedPlaywright[browserType]);

    /// <summary>
    /// Gets the chromium browser.
    /// </summary>
    public BrowserType Chromium => new(WrappedPlaywright.Chromium);

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
    public BrowserType Webkit => new(WrappedPlaywright.Webkit);

    /// <summary>
    /// Diposes all browsers.
    /// </summary>
    public void Dispose()
    {
        WrappedPlaywright.Dispose();
    }
}
