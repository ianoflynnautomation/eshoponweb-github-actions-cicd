using Microsoft.Playwright;

namespace Playwright.DotNet.Components;

public class Anchor : Component
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
    
    public virtual string InnerHtml => GetInnerHtmlAttribute();

    public virtual string Target => GetAttribute("target");

    public virtual string Rel => GetAttribute("rel");
}