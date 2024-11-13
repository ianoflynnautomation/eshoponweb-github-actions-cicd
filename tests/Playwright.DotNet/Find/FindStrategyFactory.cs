namespace Playwright.DotNet.Find;

public class FindStrategyFactory
{
    public static FindXPathStrategy Xpath(string xpath) => new(xpath);

    public static FindCssStrategy CssClass(string css) => new(css);

    public static FindStrategy CssClassContaining(string cssClass) => new FindClassContainingStrategy(cssClass);

    public static FindDataTestIdStrategy DataAutomationId(string value) => new(value);

}