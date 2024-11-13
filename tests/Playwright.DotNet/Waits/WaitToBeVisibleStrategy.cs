
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Playwright.Assertions;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Wait strategy for waiting until an element is visible.
/// </summary>
public class WaitToBeVisibleStrategy : WaitStrategy
{
    public WaitToBeVisibleStrategy(int? timeoutInterval = default)
        : base(timeoutInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.ElementToBeVisibleTimeout;
    }

    public override async Task WaitUntil<TComponent>(TComponent component)
    {
        await WaitUntil(component.WrappedElement);
    }

    public override async Task WaitUntil(WebElement element)
    {
        await element.Expect().LocatorAssertions.ToBeVisibleAsync(new() { Timeout = TimeoutInterval });
    }
}