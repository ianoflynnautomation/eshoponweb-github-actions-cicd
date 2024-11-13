
using Microsoft.Playwright;
using Playwright.DotNet.Find;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a select dropdown on the page
/// </summary>
public class Select : Component
{

    public override Type ComponentType => GetType();

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }

    public virtual ComponentsList<Option> GetAllOptions()
    {
        return this.CreateAllByXpath<Option>(".//option");
    }

    public virtual async Task SelectByTextAsync(string text)
    {
        await InternalSelect(new SelectOptionValue() { Label = text });
    }

    public virtual async Task SelectByValueAsync(string value)
    {
        await InternalSelect(new SelectOptionValue() { Value = value });
    }

    public virtual Task<bool> IsDisabled => GetDisabledAttributeAsync();

    public virtual Task<bool> IsRequired => GetRequiredAttributeAsync();

    public virtual Task<bool> IsReadonly => GetReadonlyAttributeAsync();

    private async Task InternalSelect(SelectOptionValue option)
    {
        try
        {
            await WrappedElement.WrappedLocator.SelectOptionAsync(option);

        }

        catch
        {
            throw;
        }
    }

}