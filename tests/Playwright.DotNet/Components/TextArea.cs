

using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a text area on the page
/// </summary>
public class TextArea : Component, IComponentText, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public new virtual async Task<string> GetTextAsync()
    {
        var text = await GetTextAsync();

        if (string.IsNullOrEmpty(text))
        {
            return await GetAttributeAsync("value");
        }

        return text;
    }

    public virtual async void SetTextAsync(string value) => await DefaultSetText(value);

    public async new Task HoverAsync()
    {
        await HoverAsync();
    }

     public virtual  Task<string> InnerText => GetInnerTextAsync();

   public virtual  Task<bool> IsDisabled => GetDisabledAttributeAsync();

    public virtual Task<bool> IsAutoComplete => GetAutoCompleteAttributeAsync();

    public virtual Task<bool> IsReadonly => GetReadonlyAttributeAsync();

    public virtual Task<bool> IsRequired => GetRequiredAttributeAsync();

    public virtual Task<string?> Placeholder => GetPlaceholderAttributeAsync();

    public virtual Task<int?> MaxLength => DefaultGetMaxLengthAsync();

    public virtual Task<int?> MinLength => DefaultGetMinLengthAsync();

    // public virtual int? Rows => string.IsNullOrEmpty(GetAttribute("rows")) ? null : (int?)int.Parse(GetAttribute("rows"));

    // public virtual int? Cols => string.IsNullOrEmpty(GetAttribute("cols")) ? null : (int?)int.Parse(GetAttribute("cols"));

    // public virtual string? SpellCheck => string.IsNullOrEmpty(GetAttribute("spellcheck")) ? null : GetAttribute("spellcheck");

    // public virtual string? Wrap => string.IsNullOrEmpty(GetAttribute("wrap")) ? null : GetAttribute("wrap");
}
