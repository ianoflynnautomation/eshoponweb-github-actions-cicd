
namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a text input on the page
/// </summary>
public class TextInput : Component
{
    public override Type ComponentType => GetType();

    public virtual async Task SetTextAsync(string value)
    {
        await DefaultSetText(value);
    }

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }
    
    public virtual Task<string> InnerText => GetInnerTextAsync();
    public virtual Task<string> InnerHtml => GetInnerHtmlAttributeAsync();
    public virtual Task<bool> IsDisabled => GetDisabledAttributeAsync();
    public virtual Task<string> Value => DefaultGetValueAsync();
    public virtual Task<bool> IsAutoComplete => GetAutoCompleteAttributeAsync();
    public virtual Task<bool> IsReadonly => GetReadonlyAttributeAsync();
    public virtual Task<bool> IsRequired => GetRequiredAttributeAsync();
    public virtual Task<string?> Placeholder => GetPlaceholderAttributeAsync();
    public virtual Task<int?> MaxLength => DefaultGetMaxLengthAsync();
    public virtual Task<int?> MinLength => DefaultGetMinLengthAsync();

    // public virtual int? Size => GetSizeAttribute();

}
