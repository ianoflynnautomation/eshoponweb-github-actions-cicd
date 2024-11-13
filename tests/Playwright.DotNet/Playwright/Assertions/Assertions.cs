using Playwright.DotNet.Playwright.Core.Elements;
using Playwright.DotNet.Playwright.Core;

namespace Playwright.DotNet.Playwright.Assertions;

/// <summary>
/// Represents the assertions.
/// </summary>
public static class Assertions
{
    /// <summary>
    /// Asserts the element.
    /// </summary>
    /// <param name="element">The element to assert</param>
    public static WebElementAssertions Expect(this WebElement element) => new(element);

    /// <summary>
    /// Asserts the page.
    /// </summary>
    /// <param name="page">Page to assert</param>
    public static BrowserPageAssertions Expect(this BrowserPage page) => new(page);
}
