using Playwright.DotNet.Playwright.Core;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Find;
public class FindDataTestIdStrategy(string value) : FindStrategy(value)
{
    public override WebElement Resolve(BrowserPage searchContext)
    {
        if (!Value.StartsWith("[data-testid=")) return searchContext.Locate($"[data-testid='{Value}']");
        return searchContext.Locate($"{Value}");
    }

    public override WebElement Resolve(WebElement searchContext)
    {
        if (!Value.StartsWith("[data-testid=")) return searchContext.Locate($"[data-testid='{Value}']");
        return searchContext.Locate($"{Value}");
    }

    public override string ToString()
    {
        return $"[data-testid={Value}]";
    }
}