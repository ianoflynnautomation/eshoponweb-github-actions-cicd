using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Option : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

    public virtual Task<string> InnerText => GetInnerTextAsync();
    
    public virtual Task<string> InnerHtml => GetInnerHtmlAttributeAsync();

    public virtual Task<bool> IsDisabled => GetDisabledAttributeAsync();

    public virtual Task<string> Value => DefaultGetValueAsync();
    
    public virtual Task<bool> IsSelected =>  WrappedElement.WrappedLocator.EvaluateAsync<bool>("el => el.selected");

    public virtual async Task SelectAsync()
    {
        await WrappedElement.WrappedLocator.EvaluateAsync("el => el.selected = true");
    }

    public virtual async Task UnSelectAsync()
    {
        await WrappedElement.WrappedLocator.EvaluateAsync("el => el.selected = false");
    }
}