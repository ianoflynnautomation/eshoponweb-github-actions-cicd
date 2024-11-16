using Microsoft.Playwright;

namespace Playwright.DotNet.PageObjectModels;

public class WebPage(IPage _page)
{
    protected IPage Page { get; } = _page;

    protected IPageAssertions Expect()
    {
        return Assertions.Expect(Page);
    }

    protected ILocatorAssertions Expect(ILocator locator)
    {
        return Assertions.Expect(locator);
    }

}
