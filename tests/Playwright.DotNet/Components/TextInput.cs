
using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a text input on the page
/// </summary>
public class TextInput : Component
{
    public override Type ComponentType => GetType();

    public virtual void SetText(string value)
    {
        DefaultSetText(value);
    }

    public virtual void Hover()
    {
        Hover();
    }

    public virtual string InnerText => GetInnerText();

    public virtual string InnerHtml => GetInnerHtmlAttribute();

    public virtual bool IsDisabled => GetDisabledAttribute();

    public virtual string Value => DefaultGetValue();

    public virtual bool IsAutoComplete => GetAutoCompleteAttribute();

    public virtual bool IsReadonly => GetReadonlyAttribute();

    public virtual bool IsRequired => GetRequiredAttribute();

    public virtual string Placeholder => GetPlaceholderAttribute();

    public virtual int? MaxLength => DefaultGetMaxLength();

    public virtual int? MinLength => DefaultGetMinLength();

    public new virtual int? Size => GetSizeAttribute();

}
