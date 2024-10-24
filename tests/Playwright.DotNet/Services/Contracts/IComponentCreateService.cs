
using Playwright.DotNet.Components;
using Playwright.DotNet.Find;

namespace Playwright.DotNet.Services.Contracts;

public interface IComponentCreateService
{
    TComponent Create<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component;

    ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component;
}