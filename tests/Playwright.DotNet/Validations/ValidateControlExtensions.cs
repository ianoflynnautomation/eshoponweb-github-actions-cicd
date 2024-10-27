
using Playwright.DotNet.Components;
using Playwright.DotNet.Components.Contracts;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.DI;
using Playwright.DotNet.Services;
using Playwright.DotNet.Utilities;

namespace Playwright.DotNet.Validations;

/// <summary>
/// Extensions for control validations.
/// </summary>
public static partial class ValidateControlExtensions
{
    private static void WaitUntil(Func<bool> waitCondition, string exceptionMessage, int? timeoutInSeconds, int? sleepIntervalInSeconds)
    {
        var localTimeout = timeoutInSeconds ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.ValidationsTimeout;
        var localSleepInterval = sleepIntervalInSeconds ?? ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.SleepInterval;
        var wrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();
        try
        {
            Wait.Until(waitCondition, localTimeout, exceptionMessage, localSleepInterval);
        }
        catch (TimeoutException)
        {
            throw;
        }
    }

    public static void ValidateTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
       where T : IComponentText, IComponent
    {
        WaitUntil(() => control.GetText().Equals(value), $"The control's text should be '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
    }

    public static void ValidateTextContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
      where T : IComponentText, IComponent
    {
        WaitUntil(() => control.GetText().Contains(value), $"The control's text should contain '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
    where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.InnerText.Equals(value), $"The control's inner text should be '{value}' but was '{control.InnerText}'.", timeout, sleepInterval);
    }

    public static void ValidateInnerTextContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.InnerText.Contains(value), $"The control's inner text should contain '{value}' but was '{control.InnerText}'.", timeout, sleepInterval);
    }

    public static void ValidateTitleIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
    where T : Component
    {
        WaitUntil(() => control.GetTitle().Equals(value), $"The control's title should be '{value}' but was '{control.GetTitle()}'.", timeout, sleepInterval);
    }


}