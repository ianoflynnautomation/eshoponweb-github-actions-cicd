
using Playwright.DotNet.Components;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.Services.Contracts;

public interface IComponentWaitService
{
    void Wait<TUntil, TComponent>(TComponent component, TUntil until)
        where TUntil : WaitStrategy
        where TComponent : Component;

}
