

using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a button on the page
/// </summary>
public class Button : Component
{
    public override Type ComponentType => GetType();

    public async Task ClickAsync(LocatorClickOptions? options = null)
    {
        await DefaultClickAsync(options);
    }

    public async new Task HoverAsync()
    {
        await HoverAsync();
    }

    public virtual Task<string> InnerText => GetInnerTextAsync();

    public virtual Task<string> Value => DefaultGetValueAsync();

    public virtual Task<bool> IsDisabled => GetDisabledAttributeAsync();

}

