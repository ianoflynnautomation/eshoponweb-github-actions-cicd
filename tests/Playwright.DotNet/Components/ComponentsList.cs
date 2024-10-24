using System.Collections;
using Playwright.DotNet.DI;
using Playwright.DotNet.Find;
using Playwright.DotNet.Services;

namespace Playwright.DotNet.Components;

public class ComponentsList<TComponent> : IEnumerable<TComponent>
    where TComponent : Component
{
    private readonly List<TComponent> _components;

    public ComponentsList(FindStrategy by, Component parenTComponent) 
        : this((ComponentRepository.CreateComponentListWithParent<TComponent>(by, parenTComponent)))
    {
    }

    public ComponentsList(FindStrategy by)
        : this((ComponentRepository.CreateComponentList<TComponent>(by)))
    {
    }

    public ComponentsList()
    {
        _components = new List<TComponent>();
        WrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();
    }

    public ComponentsList(IEnumerable<TComponent> componentList)
    {
        _components = new List<TComponent>(componentList);
        WrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();
    }

    public WrappedBrowser WrappedBrowser { get; }

    public TComponent this[int i]
    {
        get
        {
            try
            {
                return GetComponents().ElementAt(i);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new Exception($"Component at position {i} was not found.", ex);
            }
        }
    }

    public IEnumerator<TComponent> GetEnumerator() => GetComponents().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => _components.Count;

    public void ForEach(Action<TComponent> action)
    {
        foreach (var tableRow in this)
        {
            action(tableRow);
        }
    }

    public void Add(TComponent element)
    {
        _components.Add(element);
    }

    private IEnumerable<TComponent> GetComponents()
    {
        foreach (var component in _components)
        {
            yield return component;
        }
    }

    public void AddRange(List<TComponent> currentFilteredCells)
    {
        _components.AddRange(currentFilteredCells);
    }
}