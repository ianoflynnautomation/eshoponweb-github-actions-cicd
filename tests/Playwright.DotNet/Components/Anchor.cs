using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

public class Anchor : Component
{
    public override Type ComponentType => GetType();

    public async Task ClickAsync(LocatorClickOptions? options = null)
    {
        await DefaultClickAsync(options);
    }

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }
    public virtual Task<string> InnerText => GetInnerTextAsync();

    public virtual Task<string> InnerHtml => GetInnerHtmlAttributeAsync();

    public virtual Task<string> Target => GetAttributeAsync("target");

    public virtual Task<string> Rel => GetAttributeAsync("rel");
}