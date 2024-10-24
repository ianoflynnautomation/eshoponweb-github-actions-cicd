﻿
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.SyncPlaywright.Assertions;
using Playwright.DotNet.SyncPlaywright.Core.Elements;

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
    public WaitToBeClickableStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings.InMilliseconds().ElementToBeClickableTimeout;
    }

    /// <summary>
    /// Wait until the component is clickable.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component"></param>
    public override void WaitUntil<TComponent>(TComponent component)
    {
        WaitUntil(component);
    }

    /// <summary>
    /// Wait until the element is clickable.
    /// </summary>
    /// <param name="element"></param>
    public override void WaitUntil(WebElement element)
    {
        element.Expect().ToBeEnabled(new() { Timeout = TimeoutInterval });
    }
}