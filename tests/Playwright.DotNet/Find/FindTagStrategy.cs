using Playwright.DotNet.Playwright.Core;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Find;

public class FindTagStrategy(string value) : FindStrategy(value)
{
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