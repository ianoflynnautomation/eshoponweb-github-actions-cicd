
using Playwright.DotNet.Components.Contracts;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.Playwright.Assertions;

namespace Playwright.DotNet.Validations;

/// <summary>
/// Extensions for control validations.
/// </summary>
public static partial class ValidateControlExtensions
{
    private static int? _validationsTimeout = ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.ValidationsTimeout;

    public static async Task ValidateInnerTextIs<T>(this T control, string value, int? timeoutInterval = default)
    where T : IComponentInnerText, IComponent
    {
        await control.WrappedElement.Expect().LocatorAssertions.ToHaveTextAsync(value, new() { Timeout = timeoutInterval ?? _validationsTimeout });
    }

}