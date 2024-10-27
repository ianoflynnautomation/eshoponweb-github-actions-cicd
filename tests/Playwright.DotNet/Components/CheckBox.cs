
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
    public virtual void Check(LocatorCheckOptions? options = default)
    {
        DefaultCheck(options);
    }

    /// <summary>
    /// Automatically performs a validation if the component is already unchecked.
    /// </summary>
    /// <param name="options"></param>
    public virtual void Uncheck(LocatorUncheckOptions? options = default)
    {
        DefaultUncheck(options);
    }

    public new virtual void Hover()
    {
        Hover();
    }

    public virtual bool IsDisabled => GetDisabledAttribute();

    public virtual string Value => DefaultGetValue();

    public virtual bool IsChecked => WrappedElement.IsChecked();
}