using Playwright.DotNet.SyncPlaywright.Core;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Playwright.DotNet.SyncPlaywright.Assertions;

/// <summary>
/// Represents the browser page assertions.
/// </summary>
public class BrowserPageAssertions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserPageAssertions"/> class.
    /// </summary>
    /// <param name="page">Page to assert</param>
    public BrowserPageAssertions(BrowserPage page)
    {
        Page = page;
        NativeAssertions = Microsoft.Playwright.Assertions.Expect(page.WrappedPage);
    }

    /// <summary>
    /// Gets the page.
    /// </summary>
    public BrowserPage Page { get; init; }

    /// <summary>
    /// Gets the native assertions.
    /// </summary>
    public IPageAssertions NativeAssertions { get; init; }

    public BrowserPageAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

    /// <summary>
    /// Asserts the page to have title.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="options"></param>
    public void ToHaveTitle(string title, PageAssertionsToHaveTitleOptions options = null)
    {
        NativeAssertions.ToHaveTitleAsync(title, options);
    }

    /// <summary>
    /// Asserts the page to have title.
    /// </summary>
    /// <param name="regExp"></param>
    /// <param name="options"></param>
    public void ToHaveTitle(Regex regExp, PageAssertionsToHaveTitleOptions options = null)
    {
        NativeAssertions.ToHaveTitleAsync(regExp, options);
    }

    /// <summary>
    /// Asserts the page to have URL.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="options"></param>
    public void ToHaveURL(string url, PageAssertionsToHaveURLOptions options = null)
    {
        NativeAssertions.ToHaveURLAsync(url, options);
    }

    /// <summary>
    /// Asserts the page to have URL.
    /// </summary>
    /// <param name="regExp"></param>
    /// <param name="options"></param>
    public void ToHaveURL(Regex regExp, PageAssertionsToHaveURLOptions options = null)
    {
        NativeAssertions.ToHaveURLAsync(regExp, options);
    }
}
