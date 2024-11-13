
using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Heading : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public static new async Task HoverAsync()
    {
        await HoverAsync();
    }

    public virtual Task<string> InnerText => GetInnerTextAsync();
}
