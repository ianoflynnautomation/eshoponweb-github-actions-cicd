
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Playwright.Assertions;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Wait strategy for waiting until an element exists.
/// </summary>
public class WaitToExistStrategy : WaitStrategy
{
    public WaitToExistStrategy(int? timeoutInterval = default)
        : base(timeoutInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.ElementToExistTimeout;
    }

    public override async Task WaitUntil<TComponent>(TComponent component)
    {
        await WaitUntil(component.WrappedElement);
    }

    public override async Task WaitUntil(WebElement element)
    {
        await element.Expect().LocatorAssertions.ToBeAttachedAsync(new() { Timeout = TimeoutInterval });
    }
}