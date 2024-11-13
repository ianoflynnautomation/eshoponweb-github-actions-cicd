using Playwright.DotNet.Playwright.Core;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Find;

public class FindXPathStrategy(string value) : FindStrategy(value)
{
    public override WebElement Resolve(WebElement searchContext)
    {
        if (!Value.StartsWith("xpath=")) return searchContext.Locate($"xpath={Value}");
        return searchContext.Locate($"{Value}");
    }

    public override WebElement Resolve(BrowserPage searchContext)
    {
        if (!Value.StartsWith("xpath=")) return searchContext.Locate($"xpath={Value}");
        return searchContext.Locate($"{Value}");
    }

    public override string ToString()
    {
        return $"XPath = {Value}";
    }
}