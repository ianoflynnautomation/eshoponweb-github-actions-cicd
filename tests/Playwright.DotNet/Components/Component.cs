using Microsoft.Playwright;
using Playwright.DotNet.Components.Contracts;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.DI;
using Playwright.DotNet.Find;
using Playwright.DotNet.Services;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.Playwright.Core.Elements;
using Playwright.DotNet.Waits;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents the component on the page.
/// </summary>
public class Component : IComponent
{
    protected  WebElement _wrappedElement;
    private readonly IComponentWaitService _elementWaiter;
    private readonly List<WaitStrategy> _untils;

    public Component()
    {
        _elementWaiter = ServiceLocator.Resolve<IComponentWaitService>();
        WrappedBrowser = ServiceLocator.Resolve<WrappedBrowser>();
        _untils = [];
        BrowserService  = ServiceLocator.Resolve<IBrowserService>();
        ComponentCreateService = ServiceLocator.Resolve<IComponentCreateService>();
    }

    public WrappedBrowser WrappedBrowser { get; }

    public WebElement WrappedElement
    {
        get
        {
            WaitToBe().SyncResult();
            return _wrappedElement;
        }
        set => _wrappedElement = value;
    }

    public Component ParentComponent { get; set; }
    protected readonly IBrowserService BrowserService;
    protected readonly IComponentCreateService ComponentCreateService;

    public FindStrategy By { get; internal set; }

    public dynamic Create<TBy>(TBy by, Type newElementType)
        where TBy : FindStrategy
    {
        dynamic element;

        element = ComponentRepository.CreateComponentWithParent(by, this, newElementType);

        return element;
    }

    public TComponent Create<TComponent, TBy>(TBy by, bool shouldCacheElement = false)
    where TBy : FindStrategy
    where TComponent : Component
    {

        TComponent component;

        component = ComponentRepository.CreateComponentWithParent<TComponent>(by, this);

        return component;
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
    where TBy : FindStrategy
    where TComponent : Component
    {

        var elementsCollection = new ComponentsList<TComponent>(by, this);

        return elementsCollection;
    }

    public async Task WaitToBe()
    {
        if (_untils.Count == 0 || _untils[0] == null)
        {
            await _wrappedElement.WrappedLocator.WaitForAsync(new() { State = WaitForSelectorState.Attached, Timeout = ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().ElementToExistTimeout });
            return;
        }

        foreach (var item in _untils)
        {
            await item.WaitUntil(_wrappedElement);
        }
    }

    public async Task<string> GetAttributeAsync(string name)
    {
        return await WrappedElement.WrappedLocator.GetAttributeAsync(name)
        ?? throw new Exception($"Attribute {name} not found");
    }

    private async Task ScrollIntoViewIfNeededAsync(bool shouldWait = true)
    {

        try
        {
           await WrappedElement.WrappedLocator.ScrollIntoViewIfNeededAsync();
        }
        catch (Exception)
        {
            // ignore
        }
    }

    public async Task ScrollToVisible()
    {
        await ScrollIntoViewIfNeededAsync(true);
    }

    public void EnsureState(WaitStrategy until)
    {
        _untils.Add(until);
    }

    public virtual TComponent As<TComponent>()
    where TComponent : Component
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = By;

        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, WrappedElement);
        }
        else
        {
            component.WrappedElement = WrappedElement;
        }

        return component;
    }


    public string ComponentName { get; internal set; }

    public string PageName { get; internal set; }

    public virtual Type ComponentType => GetType();

    public Type LocatorType => By.GetType();

    public string LocatorValue => By.Value;


    public async Task<string?> GetTitleAsync()
        => string.IsNullOrEmpty(await GetAttributeAsync("title")) 
        ? null 
        : await GetAttributeAsync("title");
    

    internal async Task DefaultClickAsync(LocatorClickOptions? options = null)
    {
        if (options != null)
        {
            if (options.Force != null && (bool)options.Force)
            {
                await PerformJsClickAsync();
            }
            else
            {
                await WrappedElement.WrappedLocator.ClickAsync(options);
            }
        }

        else await WrappedElement.WrappedLocator.ClickAsync();
    }

    private async Task PerformJsClickAsync() => await WrappedElement.WrappedLocator.EvaluateAsync("el => el.click()");

    internal async Task DefaultCheckAsync(LocatorCheckOptions? options = default)
    {
        await WrappedElement.WrappedLocator.CheckAsync(options);
    }

    internal async Task DefaultUncheckAsync(LocatorUncheckOptions? options = default)
    {
        await WrappedElement.WrappedLocator.UncheckAsync(options);
    }

    internal async Task HoverAsync()
    {
        await WrappedElement.WrappedLocator.HoverAsync();
    }

    internal async Task<string> GetInnerTextAsync()
    {
        return await WrappedElement.WrappedLocator.InnerTextAsync();
    }

    internal async Task<string> DefaultGetValueAsync()
    {
        return await GetAttributeAsync("value");
    }

    internal async Task<int?> DefaultGetMaxLengthAsync()
    {
        int? result = string.IsNullOrEmpty(await GetAttributeAsync("maxlength")) 
        ? null 
        : int.Parse(await GetAttributeAsync("maxlength"));

        if (result != null && (result == 2147483647 || result == -1))
        {
            result = null;
        }

        return result;
    }

    internal async Task<int?> DefaultGetMinLengthAsync()
    {
        int? result = string.IsNullOrEmpty(await GetAttributeAsync("minlength")) 
        ? null 
        : int.Parse(await GetAttributeAsync("minlength"));

        if (result != null && result == -1)
        {
            result = null;
        }

        return result;
    }

    internal async Task<int?> GetSizeAttributeAsync()
    {
        return string.IsNullOrEmpty(await GetAttributeAsync("size")) 
        ? null 
        : int.Parse(await GetAttributeAsync("size"));
    }

    internal async Task<int?> GetHeightAttributeAsync()
    {
        return string.IsNullOrEmpty(await GetAttributeAsync("height")) 
        ? null 
        : int.Parse(await GetAttributeAsync("height"));
    }

    internal async Task<int?> GetWidthAttributeAsync()
    {
        return string.IsNullOrEmpty(await GetAttributeAsync("width"))
        ? null 
        : int.Parse(await GetAttributeAsync("width"));
    }

    internal async Task<string> GetInnerHtmlAttributeAsync()
    {
        return await WrappedElement.WrappedLocator.InnerHTMLAsync();
    }

    internal async Task<string?> GetForAttributeAsync()
    {
        return string.IsNullOrEmpty(await GetAttributeAsync("for")) 
        ? null
        : await GetAttributeAsync("for");
    }

    protected async Task<bool> GetDisabledAttributeAsync()
    {
        return await WrappedElement.WrappedLocator.IsDisabledAsync();
    }

    internal async Task<string> GetTextAsync()
    {
        return await WrappedElement.WrappedLocator.InnerTextAsync();
    }

    internal async Task<string?> GetPlaceholderAttributeAsync()
    {
        return string.IsNullOrEmpty(await GetAttributeAsync("placeholder"))
        ? null
        : await GetAttributeAsync("placeholder");
    }

    internal async Task<bool> GetAutoCompleteAttributeAsync()
    {
        return await GetAttributeAsync("autocomplete") == "on";
    }

    internal async Task<bool> GetReadonlyAttributeAsync()
    {
        return !string.IsNullOrEmpty(await GetAttributeAsync("readonly"));
    }

    internal  async Task<bool> GetRequiredAttributeAsync()
    {
        return !string.IsNullOrEmpty(await GetAttributeAsync("required"));
    }

    internal async Task DefaultSetText(string value, LocatorFillOptions? options = default)
    {
        await WrappedElement.WrappedLocator.FillAsync(value, options);
    }

}
