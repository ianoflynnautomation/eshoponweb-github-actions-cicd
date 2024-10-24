

using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a button on the page
/// </summary>
public class Button : Component
{
    public override Type ComponentType => GetType();

 
    public virtual void Click(LocatorClickOptions options = null)
    {
        DefaultClick(options);
    }

    public virtual void Hover()
    {
        Hover();
    }
    public virtual string InnerText => GetInnerText();

    public virtual string Value => DefaultGetValue();

    public virtual bool IsDisabled => GetDisabledAttribute();

}

