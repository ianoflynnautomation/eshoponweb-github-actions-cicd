using Playwright.DotNet.DI;
using Playwright.DotNet.Services;
using Playwright.DotNet.Playwright.Core;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Find;

public abstract class FindStrategy(string value)
{
    public string Value { get; } = value;

    public abstract WebElement Resolve(BrowserPage searchContext);

    public abstract WebElement Resolve(WebElement searchContext);

    protected WrappedBrowser WrappedBrowser => ServiceLocator.Resolve<WrappedBrowser>();
}