
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.WebApi;
using System.Text.Json;
using Playwright.DotNet.SyncPlaywright.GetByOptions;

namespace Playwright.DotNet.Playwright.Core.Elements;

/// <summary>
/// Wrapper for ILocator with synchronous methods.
/// </summary>
public class WebElement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebElement"/> class.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="locator"></param>
    internal WebElement(BrowserPage page, ILocator locator)
    {
        Page = page;
        WrappedLocator = locator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebElement"/> class.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="element"></param>
    internal WebElement(BrowserPage page, WebElement element)
    {
        Page = page;
        WrappedLocator = element.WrappedLocator;
    }

    /// <summary>
    /// Gets the wrapped locator.
    /// </summary>
    public ILocator WrappedLocator { get; set; }

    /// <summary>
    /// Gets the first element.
    /// </summary>
    public virtual WebElement First => new WebElement(Page, WrappedLocator.First);

    /// <summary>
    /// Gets the last element.
    /// </summary>
    public virtual WebElement Last => new WebElement(Page, WrappedLocator.Last);

    /// <summary>
    /// Gets the page.
    /// </summary>
    public BrowserPage Page { get; internal init; }

    /// <summary>
    /// Gets the parent element.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<WebElement> All()
    {
        IReadOnlyCollection<ILocator> nativeLocators;

        try
        {
            // Maybe because of the async to sync conversion, it always fails on the first try.
            // Another reason could be the fact that Playwright doesn't wait for all elements to be loaded properly on the page before trying to find them.
            nativeLocators = WrappedLocator.AllAsync().Result;
        }
        catch
        {
            // patch: Adding a wait for no HTTP requests and responses sent in the last few hundred milliseconds.
            //ServicesCollection.Current.Resolve<WrappedBrowser>().CurrentPage.WaitForLoadState(LoadState.NetworkIdle);

            nativeLocators = WrappedLocator.AllAsync().Result;
        }

        var elements = new List<WebElement>();

        foreach (var locator in nativeLocators)
        {
            elements.Add(new WebElement(Page, locator));
        }

        return elements;
    }


    public virtual WebElement GetByTestId(string testId)
    {
        return new WebElement(Page, WrappedLocator.GetByTestId(testId));
    }

    public virtual WebElement GetByTestId(Regex testId)
    {
        return new WebElement(Page, WrappedLocator.GetByTestId(testId));
    }


    public virtual WebElement Locate(string selectorOrElement)
    {
        return new WebElement(Page, WrappedLocator.Locator(selectorOrElement));
    }

    public virtual WebElement Locate(WebElement selectorOrElement)
    {
        return new WebElement(Page, WrappedLocator.Locator(selectorOrElement.WrappedLocator));
    }

    public virtual FrameElement LocateFrame(string selector)
    {
        return new FrameElement(Page, Locate(selector));
    }
}
