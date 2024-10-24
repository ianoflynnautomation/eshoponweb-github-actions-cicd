

using Microsoft.Playwright;
using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a button on the page
/// </summary>
public class Div : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public virtual void Hover()
    {
        Hover();
    }
    public virtual string InnerText => GetInnerText();

    public virtual string InnerHtml => GetInnerHtmlAttribute();


}
