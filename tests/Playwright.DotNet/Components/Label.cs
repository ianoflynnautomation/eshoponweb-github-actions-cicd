using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Label : Component, IComponentInnerText
{
    public new virtual void Hover()
    {
        Hover();
    }
    public override Type ComponentType => GetType();

    public virtual string InnerText => GetInnerText();

    public virtual string InnerHtml => GetInnerHtmlAttribute();

    public virtual string For => GetForAttribute();
}