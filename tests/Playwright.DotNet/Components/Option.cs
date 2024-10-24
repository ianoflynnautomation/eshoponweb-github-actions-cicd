using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Option : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public virtual string InnerText => GetInnerText();

    public virtual bool IsDisabled => GetDisabledAttribute();

    public virtual string Value => DefaultGetValue();
    
    public virtual bool IsSelected => WrappedElement.Evaluate<bool>("el => el.selected");

    public virtual void Select()
    {
        WrappedElement.Evaluate("el => el.selected = true");
    }

    public virtual void UnSelect()
    {
        WrappedElement.Evaluate("el => el.selected = false");
    }
}