

using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a text area on the page
/// </summary>
public class TextArea : Component, IComponentText, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public new virtual string GetText()
    {
        var text = base.GetText();

        if (string.IsNullOrEmpty(text))
        {
            return GetAttribute("value");
        }

        return text;
    }

    public virtual void SetText(string value) => DefaultSetText(value);

    public new virtual void Hover() => Hover();

    public virtual string InnerText => GetInnerText();

    public virtual bool IsDisabled => GetDisabledAttribute();

    public virtual bool IsAutoComplete => GetAutoCompleteAttribute();

    public virtual bool IsReadonly => GetReadonlyAttribute();

    public virtual bool IsRequired => GetRequiredAttribute();

    public virtual string? Placeholder => GetPlaceholderAttribute();

    public virtual int? MaxLength => DefaultGetMaxLength();

    public virtual int? MinLength => DefaultGetMinLength();

    public virtual int? Rows => string.IsNullOrEmpty(GetAttribute("rows")) ? null : (int?)int.Parse(GetAttribute("rows"));

    public virtual int? Cols => string.IsNullOrEmpty(GetAttribute("cols")) ? null : (int?)int.Parse(GetAttribute("cols"));

    public virtual string? SpellCheck => string.IsNullOrEmpty(GetAttribute("spellcheck")) ? null : GetAttribute("spellcheck");

    public virtual string? Wrap => string.IsNullOrEmpty(GetAttribute("wrap")) ? null : GetAttribute("wrap");
}
