
using Playwright.DotNet.Components.Contracts;
using Playwright.DotNet.Playwright.Assertions;

namespace Playwright.DotNet.Validations;

/// <summary>
/// Extensions for control validations.
/// </summary>
public static partial class ValidateControlExtensions
{
    public static async Task ValidateInnerTextIs<T>(this T control, string value)
    where T : IComponentInnerText, IComponent
    {
        await control.WrappedElement.Expect().LocatorAssertions.ToHaveTextAsync(value);
    }

}