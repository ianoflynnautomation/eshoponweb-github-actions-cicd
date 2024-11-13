using Microsoft.Playwright;
using Playwright.DotNet.Playwright.Core.Elements;
using Playwright.DotNet.Playwright.Interactions;

namespace Playwright.DotNet.Playwright.Core;

/// <summary>
/// Synchronous wrapper for Playwright IPage.
/// </summary>
public partial class BrowserPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserPage"/> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="page"></param>
    internal BrowserPage(BrowserContext context, IPage page)
    {
        Context = context;
        WrappedPage = page;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserPage"/> class.
    /// </summary>
    /// <param name="page"></param>
    internal BrowserPage(IPage page)
    {
        WrappedPage = page;
    }

    /// <summary>
    /// Gets the wrapped page.
    /// </summary>
    public IPage WrappedPage { get; internal init; }

    /// <summary>
    /// Gets the context.
    /// </summary>
    public BrowserContext? Context { get; internal init; }

    /// <summary>
    /// Gets the url.
    /// </summary>
    public string Url => WrappedPage.Url;

    /// <summary>
    /// Gets the frames.
    /// </summary>
    public IReadOnlyList<IFrame> Frames => WrappedPage.Frames;

    /// <summary>
    /// Checks if the page is closed.
    /// </summary>
    public bool IsClosed => WrappedPage.IsClosed;

    /// <summary>
    /// gets the keyboard.
    /// </summary>
    public Keyboard Keyboard => new Keyboard(WrappedPage.Keyboard);

    /// <summary>
    /// Gets the main frame.
    /// </summary>
    public IFrame MainFrame => WrappedPage.MainFrame;

    /// <summary>
    /// Gets the mouse.
    /// </summary>
    public Mouse Mouse => new(WrappedPage.Mouse);

    /// <summary>
    /// Gets the api request.
    /// </summary>
    public IAPIRequestContext APIRequest => WrappedPage.APIRequest;

    /// <summary>
    /// Gets the touch screen.
    /// </summary>
    public ITouchscreen Touchscreen => WrappedPage.Touchscreen;

    /// <summary>
    /// Gets the video.
    /// </summary>
    public IVideo? Video => WrappedPage.Video;

    public PageViewportSizeResult? ViewportSize => WrappedPage.ViewportSize;

    public IReadOnlyList<IWorker> Workers => WrappedPage.Workers;

        public WebElement Locate(string selector)
    {
        return new WebElement(this, WrappedPage.Locator(selector));
    }


}
