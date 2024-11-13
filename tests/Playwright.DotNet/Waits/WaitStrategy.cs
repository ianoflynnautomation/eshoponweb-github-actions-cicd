
using Playwright.DotNet.Components;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.DI;
using Playwright.DotNet.Services;
using Playwright.DotNet.Playwright.Core.Elements;

namespace Playwright.DotNet.Waits;

/// <summary>
/// Base class for all wait strategies.
/// </summary>
/// <param name="timeoutInterval">Default timout interval in milliseconds for all wait strategies</param>
/// <param name="sleepInterval">Sleep interval timout in milliseconds for all wait strategies</param>
public abstract class WaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
{
    protected WrappedBrowser WrappedBrowser { get; } = ServiceLocator.Resolve<WrappedBrowser>();

    protected int? TimeoutInterval { get; set; } = timeoutInterval;

    protected int? SleepInterval { get; } = sleepInterval ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.SleepInterval;

    public abstract Task WaitUntil<TComponent>(TComponent by)
        where TComponent : Component;

    public abstract Task WaitUntil(WebElement element);
}