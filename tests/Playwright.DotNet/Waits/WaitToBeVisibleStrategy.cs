
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.SyncPlaywright.Assertions;
using Playwright.DotNet.SyncPlaywright.Core.Elements;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Wait strategy for waiting until an element is visible.
/// </summary>
public class WaitToBeVisibleStrategy : WaitStrategy
{
    public WaitToBeVisibleStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().ElementToBeVisibleTimeout;
    }

    public override void WaitUntil<TComponent>(TComponent component)
    {
        WaitUntil(component.WrappedElement);
    }

    public override void WaitUntil(WebElement element)
    {
        element.Expect().ToBeVisible(new() { Timeout = TimeoutInterval });
    }
}