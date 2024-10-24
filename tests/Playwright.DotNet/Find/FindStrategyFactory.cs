namespace Playwright.DotNet.Find;

public class FindStrategyFactory
{
    public FindXPathStrategy Xpath(string xpath) => new(xpath);

    public FindCssStrategy CssClass(string css) => new(css);

    public FindStrategy CssClassContaining(string cssClass) => new FindClassContainingStrategy(cssClass);

    public FindDataTestIdStrategy DataAutomationId(string value) => new(value);

}