using Playwright.DotNet.Playwright.Core.Elements;
using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Assertions;

/// <summary>
/// Represents the web element assertions.
/// </summary>
public class WebElementAssertions
{   
    /// <summary>
    /// Initializes a new instance of the <see cref="WebElementAssertions"/> class.
    /// </summary>
    /// <param name="element"></param>
    public WebElementAssertions(WebElement element)
    {
        Element = element;
        NativeAssertions = Microsoft.Playwright.Assertions.Expect(Element.WrappedLocator);
    }

    /// <summary>
    /// Gets the element.
    /// </summary>
    public WebElement Element { get; init; }

    /// <summary>
    /// Gets the native assertions.
    /// </summary>
    public ILocatorAssertions NativeAssertions { get; init; }

    public WebElementAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

}
