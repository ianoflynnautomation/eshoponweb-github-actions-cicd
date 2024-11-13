using Playwright.DotNet.Playwright.Core;
using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Assertions;

/// <summary>
/// Represents the browser page assertions.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BrowserPageAssertions"/> class.
/// </remarks>
/// <param name="page">Page to assert</param>
public class BrowserPageAssertions(BrowserPage page)
{

    /// <summary>
    /// Gets the page.
    /// </summary>
    public BrowserPage Page { get; init; } = page;

    /// <summary>
    /// Gets the native assertions.
    /// </summary>
    public IPageAssertions NativeAssertions { get; init; } = Microsoft.Playwright.Assertions.Expect(page.WrappedPage);

    public BrowserPageAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

}
