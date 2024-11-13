
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Playwright.Assertions;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Wait strategy for waiting until an element is clickable.
/// </summary>
public class WaitToBeClickableStrategy : WaitStrategy
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaitToBeClickableStrategy"/> class.
    /// </summary>
    /// <param name="timeoutInterval">Element to be clickable timout interval in milliseconds</param>
    /// <param name="sleepInterval">Element to be clickable sleep interval in milliseconds</param>
    public WaitToBeClickableStrategy(int? timeoutInterval = default)
        : base(timeoutInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.ElementToBeClickableTimeout;
    }

    /// <summary>
    /// Wait until the component is clickable.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component"></param>
    public override async Task WaitUntil<TComponent>(TComponent component)
    {
        await WaitUntil(component);
    }

    /// <summary>
    /// Wait until the element is clickable.
    /// </summary>
    /// <param name="element"></param>
    public override async Task WaitUntil(WebElement element)
    {
        await element.Expect().LocatorAssertions.ToBeEnabledAsync(new() { Timeout = TimeoutInterval });
    }
}