using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Label : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }

    public virtual  Task<string> InnerText => GetInnerTextAsync();


    public virtual Task<string> InnerHtml => GetInnerHtmlAttributeAsync();

    public virtual Task<string?> For => GetForAttributeAsync();
}