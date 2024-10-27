
using Playwright.DotNet.Components.Contracts;

namespace Playwright.DotNet.Components;

public class Heading : Component, IComponentInnerText
{
    public override Type ComponentType => GetType();

     public new virtual void Hover()
    {
        Hover();
    }
    public virtual string InnerText => GetInnerText();

}
