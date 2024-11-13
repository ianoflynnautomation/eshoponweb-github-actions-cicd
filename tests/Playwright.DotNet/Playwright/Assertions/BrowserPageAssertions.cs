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
public class BrowserPageAssertions
{

    public BrowserPageAssertions(BrowserPage page)
    {
        Page = page;
        PageAssertions = Microsoft.Playwright.Assertions.Expect(page.WrappedPage);

    }

    /// <summary>
    /// Gets the page.
    /// </summary>
    public BrowserPage Page { get; init; }

    /// <summary>
    /// Gets the native assertions.
    /// </summary>
    public IPageAssertions PageAssertions { get; init; }

    public BrowserPageAssertions Not
    {
        get
        {
            _ = PageAssertions.Not;
            return this;
        }
    }

}
