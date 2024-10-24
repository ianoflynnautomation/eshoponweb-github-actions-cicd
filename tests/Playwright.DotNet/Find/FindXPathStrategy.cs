using Playwright.DotNet.SyncPlaywright.Core;
using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Find;

public class FindXPathStrategy : FindStrategy
{
    public FindXPathStrategy(string value)
        : base(value)
    {
    }

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