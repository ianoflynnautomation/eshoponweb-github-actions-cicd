using Microsoft.Playwright;
using Playwright.DotNet.Components.Contracts;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;
using Playwright.DotNet.DI;
using Playwright.DotNet.Find;
using Playwright.DotNet.Services;
using Playwright.DotNet.Services.Contracts;
using Playwright.DotNet.SyncPlaywright.Core.Elements;
using Playwright.DotNet.Waits;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents the component on the page.
/// </summary>
public class Component : IComponent
{
    protected  WebElement _wrappedElement;
    private readonly IComponentWaitService _elementWaiter;
    private readonly List<WaitStrategy> _untils;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Component()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
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
            WaitToBe();
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

    public void WaitToBe()
    {
        if (_untils.Count == 0 || _untils[0] == null)
        {
            _wrappedElement.WaitFor(new() { State = WaitForSelectorState.Attached, Timeout = ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName).TimeoutSettings?.InMilliseconds().ElementToExistTimeout });
            return;
        }

        foreach (var item in _untils)
        {
            item.WaitUntil(_wrappedElement);
        }
    }

    public string GetAttribute(string name)
    {
        return WrappedElement.GetAttribute(name);
    }

    private void ScrollToBeVisible(bool shouldWait = true)
    {

        try
        {
            WrappedElement.ScrollIntoViewIfNeeded();
        }
        catch (Exception)
        {
            // ignore
        }
    }

    public void ScrollToVisible()
    {
        ScrollToBeVisible(true);
    }

    public void EnsureState(WaitStrategy until)
    {
        _untils.Add(until);
    }

    public virtual TComponent As<TComponent>()
    where TComponent : Component
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = this.By;

        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, this.WrappedElement);
        }
        else
        {
            component.WrappedElement = this.WrappedElement;
        }

        return component;
    }


    public string ComponentName { get; internal set; }

    public string PageName { get; internal set; }

    public virtual Type ComponentType => GetType();

    public Type LocatorType => By.GetType();

    public string LocatorValue => By.Value;


    public string? GetTitle() => string.IsNullOrEmpty(GetAttribute("title")) ? null : GetAttribute("title");
    

    internal void DefaultClick(LocatorClickOptions? options = null)
    {
        if (options != null)
        {
            if (options.Force != null && (bool)options.Force) PerformJsClick();
            else WrappedElement.Click(options);
        }

        else WrappedElement.Click();
    }

    private void PerformJsClick() => WrappedElement.Evaluate("el => el.click()");

    internal void DefaultCheck(LocatorCheckOptions? options = default)
    {

        WrappedElement.Check(options);

    }

    internal void DefaultUncheck(LocatorUncheckOptions? options = default)
    {

        WrappedElement.Uncheck(options);
    }

    internal void Hover()
    {

        WrappedElement.Hover();
    }

    internal string GetInnerText()
    {
        return WrappedElement.InnerText().Trim().Replace("\r\n", string.Empty);
    }

    internal string DefaultGetValue()
    {
        return GetAttribute("value");
    }

    internal int? DefaultGetMaxLength()
    {
        int? result = string.IsNullOrEmpty(GetAttribute("maxlength")) ? null : (int?)int.Parse(GetAttribute("maxlength"));
        if (result != null && (result == 2147483647 || result == -1))
        {
            result = null;
        }

        return result;
    }

    internal int? DefaultGetMinLength()
    {
        int? result = string.IsNullOrEmpty(GetAttribute("minlength")) ? null : (int?)int.Parse(GetAttribute("minlength"));

        if (result != null && result == -1)
        {
            result = null;
        }

        return result;
    }

    internal int? GetSizeAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("size")) ? null : (int?)int.Parse(GetAttribute("size"));
    }

    internal int? GetHeightAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("height")) ? null : (int?)int.Parse(GetAttribute("height"));
    }

    internal int? GetWidthAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("width")) ? null : (int?)int.Parse(GetAttribute("width"));
    }

    internal string GetInnerHtmlAttribute()
    {
        return WrappedElement.InnerHTML();
    }

    internal string? GetForAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("for")) ? null : GetAttribute("for");
    }

    protected bool GetDisabledAttribute()
    {
        return WrappedElement.IsDisabled();
    }

    internal string GetText()
    {
        return WrappedElement.InnerText();
    }

    internal string? GetPlaceholderAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");
    }

    internal bool GetAutoCompleteAttribute()
    {
        return GetAttribute("autocomplete") == "on";
    }

    internal bool GetReadonlyAttribute()
    {
        return !string.IsNullOrEmpty(GetAttribute("readonly"));
    }

    internal bool GetRequiredAttribute()
    {
        return !string.IsNullOrEmpty(GetAttribute("required"));
    }

    internal void DefaultSetText(string value, LocatorFillOptions? options = default)
    {

        WrappedElement.Fill(value, options);
    }

}
