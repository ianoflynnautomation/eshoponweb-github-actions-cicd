
using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a checkbox on the page
/// </summary>
public class CheckBox : Component
{
    /// <summary>
    /// Automatically performs a validation if the component is already checked.
    /// </summary>
    /// <param name="options"></param>
    public virtual async Task CheckAsync(LocatorCheckOptions? options = default)
    {
        await DefaultCheckAsync(options);
    }

    /// <summary>
    /// Automatically performs a validation if the component is already unchecked.
    /// </summary>
    /// <param name="options"></param>
    public virtual async Task UncheckAsync(LocatorUncheckOptions? options = default)
    {
       await DefaultUncheckAsync(options);
    }

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }
    public virtual Task<bool> IsDisabled => GetDisabledAttributeAsync();

    public virtual Task<string> Value => DefaultGetValueAsync();

    public virtual Task<bool> IsChecked => WrappedElement.WrappedLocator.IsCheckedAsync();
}