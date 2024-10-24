using Playwright.DotNet.SyncPlaywright.Core.Elements;
using Playwright.DotNet.SyncPlaywright.Core;

namespace Playwright.DotNet.SyncPlaywright.Assertions;

/// <summary>
/// Represents the assertions.
/// </summary>
public static class Assertions
{
    /// <summary>
    /// Asserts the element.
    /// </summary>
    /// <param name="element">The element to assert</param>
    public static WebElementAssertions Expect(this WebElement element) => new WebElementAssertions(element);

    /// <summary>
    /// Asserts the page.
    /// </summary>
    /// <param name="page">Page to assert</param>
    public static BrowserPageAssertions Expect(this BrowserPage page) => new BrowserPageAssertions(page);
}
