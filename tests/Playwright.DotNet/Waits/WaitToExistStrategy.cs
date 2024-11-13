
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
    public WaitToExistStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().ElementToExistTimeout;
    }

    public override async Task WaitUntil<TComponent>(TComponent component)
    {
        await WaitUntil(component.WrappedElement);
    }

    public override async Task WaitUntil(WebElement element)
    {
        await element.Expect().NativeAssertions.ToBeAttachedAsync();
    }
}