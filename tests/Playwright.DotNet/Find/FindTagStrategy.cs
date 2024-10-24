using Playwright.DotNet.SyncPlaywright.Core;
using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Find;

public class FindTagStrategy : FindStrategy
{
    public FindTagStrategy(string value)
        : base(value)
    {
    }

    public override WebElement Resolve(BrowserPage searchContext)
    {
         return searchContext.Locate($"{Value}");
    }

    public override WebElement Resolve(WebElement searchContext)
    {
         return searchContext.Locate($"{Value}");
    }

    public override string ToString()
    {
        return $"Tag = {Value}";
    }
}