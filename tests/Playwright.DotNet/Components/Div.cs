

using Microsoft.Playwright;
using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a button on the page
/// </summary>
public class Div : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public async new Task HoverAsync()
    {
        await HoverAsync();
    }

    public virtual Task<string> InnerText => GetInnerTextAsync();

    public virtual Task<string>  InnerHtml => GetInnerHtmlAttributeAsync();


}
