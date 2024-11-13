using System.Text.RegularExpressions;
using Microsoft.Playwright;


namespace Playwright.DotNet.Playwright.Core.Elements;

/// <summary>
/// Wrapper for IFrameLocator with synchronous methods.
/// </summary>
public class FrameElement : WebElement
{
    internal FrameElement(BrowserPage page, ILocator locator)
        : base(page, locator)
    {
        WrappedFrameLocator = locator.FrameLocator(":scope");
    }

    internal FrameElement(BrowserPage page, WebElement element)
        : base(page, element)
    {
        WrappedFrameLocator = element.WrappedLocator.FrameLocator(":scope");
    }

    public IFrameLocator WrappedFrameLocator { get; set; }

    public override WebElement GetByTestId(string testId)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTestId(testId));
    }

    public override WebElement GetByTestId(Regex testId)
    {
        return new WebElement(Page, WrappedFrameLocator.GetByTestId(testId));
    }
    
    public override WebElement Locate(string selectorOrElement)
    {
        return new WebElement(Page, WrappedFrameLocator.Locator(selectorOrElement));
    }

    public override WebElement Locate(WebElement selectorOrElement)
    {
        return new WebElement(Page, WrappedFrameLocator.Locator(selectorOrElement.WrappedLocator));
    }

    public override FrameElement LocateFrame(string selector)
    {
        return new FrameElement(Page, WrappedFrameLocator.Locator(selector));
    }
}
