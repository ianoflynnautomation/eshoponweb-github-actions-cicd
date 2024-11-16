using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels;

public class WebPage
{
    protected IPage Page { get; }

    public WebPage(IPage _page)
    {
        Page = _page; ;
    }

}
