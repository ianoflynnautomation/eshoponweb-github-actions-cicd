using System.Diagnostics;
using Playwright.DotNet.Components;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.Services;

public class ComponentWaitService : IComponentWaitService
{
    public void Wait<TUntil, TComponent>(TComponent component, TUntil until)
        where TUntil : WaitStrategy
        where TComponent : Component
    {
        try
        {
            WaitInternal(component, until);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
        
    }

    internal void WaitInternal<TUntil, TComponent>(TComponent by, TUntil until)
        where TUntil : WaitStrategy
        where TComponent : Component
    {
        until?.WaitUntil(@by);
    }
}