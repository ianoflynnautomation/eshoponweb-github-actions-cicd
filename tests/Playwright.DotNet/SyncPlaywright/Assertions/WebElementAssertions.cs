using Playwright.DotNet.SyncPlaywright.Core.Elements;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.SyncPlaywright.Assertions;

/// <summary>
/// Represents the web element assertions.
/// </summary>
public class WebElementAssertions
{   
    /// <summary>
    /// Initializes a new instance of the <see cref="WebElementAssertions"/> class.
    /// </summary>
    /// <param name="element"></param>
    public WebElementAssertions(WebElement element)
    {
        Element = element;
        NativeAssertions = Microsoft.Playwright.Assertions.Expect(Element.WrappedLocator);
    }

    /// <summary>
    /// Gets the element.
    /// </summary>
    public WebElement Element { get; init; }

    /// <summary>
    /// Gets the native assertions.
    /// </summary>
    public ILocatorAssertions NativeAssertions { get; init; }

    public WebElementAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

    /// <summary>
    /// Asserts the element to be attached.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeAttached(LocatorAssertionsToBeAttachedOptions? options = null)
    {
        NativeAssertions.ToBeAttachedAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be checked.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeChecked(LocatorAssertionsToBeCheckedOptions? options = null)
    {
        NativeAssertions.ToBeCheckedAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be clickable.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeDisabled(LocatorAssertionsToBeDisabledOptions? options = null)
    {
        NativeAssertions.ToBeDisabledAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be editable.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeEditable(LocatorAssertionsToBeEditableOptions? options = null)
    {
        NativeAssertions.ToBeEditableAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be empty.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeEmpty(LocatorAssertionsToBeEmptyOptions? options = null)
    {
        NativeAssertions.ToBeEmptyAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be enabled.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeEnabled(LocatorAssertionsToBeEnabledOptions? options = null)
    {
        NativeAssertions.ToBeEnabledAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be focused.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeFocused(LocatorAssertionsToBeFocusedOptions? options = null)
    {
        NativeAssertions.ToBeFocusedAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be hidden.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeHidden(LocatorAssertionsToBeHiddenOptions? options = null)
    {
        NativeAssertions.ToBeHiddenAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be in the viewport.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeInViewport(LocatorAssertionsToBeInViewportOptions? options = null)
    {
        NativeAssertions.ToBeInViewportAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to be visible.
    /// </summary>
    /// <param name="options"></param>
    public void ToBeVisible(LocatorAssertionsToBeVisibleOptions? options = null)
    {
        NativeAssertions.ToBeVisibleAsync(options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to contain text.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToContainText(string expected, LocatorAssertionsToContainTextOptions? options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to contain text.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToContainText(Regex expected, LocatorAssertionsToContainTextOptions? options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to contain text.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToContainText(IEnumerable<string> expected, LocatorAssertionsToContainTextOptions? options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to contain text.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToContainText(IEnumerable<Regex> expected, LocatorAssertionsToContainTextOptions? options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveAttribute(string name, string value, LocatorAssertionsToHaveAttributeOptions? options = null)
    {
        NativeAssertions.ToHaveAttributeAsync(name, value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveAttribute(string name, Regex value, LocatorAssertionsToHaveAttributeOptions? options = null)
    {
        NativeAssertions.ToHaveAttributeAsync(name, value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveClass(string expected, LocatorAssertionsToHaveClassOptions? options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveClass(Regex expected, LocatorAssertionsToHaveClassOptions? options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveClass(IEnumerable<string> expected, LocatorAssertionsToHaveClassOptions? options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have attribute.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveClass(IEnumerable<Regex> expected, LocatorAssertionsToHaveClassOptions? options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="count"></param>
    /// <param name="options"></param>
    public void ToHaveCount(int count, LocatorAssertionsToHaveCountOptions? options = null)
    {
        NativeAssertions.ToHaveCountAsync(count, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveCSS(string name, string value, LocatorAssertionsToHaveCSSOptions? options = null)
    {
        NativeAssertions.ToHaveCSSAsync(name, value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveCSS(string name, Regex value, LocatorAssertionsToHaveCSSOptions? options = null)
    {
        NativeAssertions.ToHaveCSSAsync(name, value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="options"></param>
    public void ToHaveId(string id, LocatorAssertionsToHaveIdOptions? options = null)
    {
        NativeAssertions.ToHaveIdAsync(id, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="options"></param>
    public void ToHaveId(Regex id, LocatorAssertionsToHaveIdOptions? options = null)
    {
        NativeAssertions.ToHaveIdAsync(id, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveJSProperty(string name, object value, LocatorAssertionsToHaveJSPropertyOptions? options = null)
    {
        NativeAssertions.ToHaveJSPropertyAsync(name, value, options).SyncResult();
    }

    /// <summary>
    ///  Asserts the element to have count.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveText(string expected, LocatorAssertionsToHaveTextOptions? options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveText(Regex expected, LocatorAssertionsToHaveTextOptions? options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveText(IEnumerable<string> expected, LocatorAssertionsToHaveTextOptions? options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();

    }

    /// <summary>
    /// Asserts the element to have count.
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="options"></param>
    public void ToHaveText(IEnumerable<Regex> expected, LocatorAssertionsToHaveTextOptions? options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveValue(string value, LocatorAssertionsToHaveValueOptions? options = null)
    {
        NativeAssertions.ToHaveValueAsync(value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public void ToHaveValue(Regex value, LocatorAssertionsToHaveValueOptions? options = null)
    {
        NativeAssertions.ToHaveValueAsync(value, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have value.
    /// </summary>
    /// <param name="values"></param>
    /// <param name="options"></param>
    public void ToHaveValues(IEnumerable<string> values, LocatorAssertionsToHaveValuesOptions? options = null)
    {
        NativeAssertions.ToHaveValuesAsync(values, options).SyncResult();
    }

    /// <summary>
    /// Asserts the element to have value.
    /// </summary>
    /// <param name="values"></param>
    /// <param name="options"></param>
    public void ToHaveValues(IEnumerable<Regex> values, LocatorAssertionsToHaveValuesOptions? options = null)
    {
        NativeAssertions.ToHaveValuesAsync(values, options).SyncResult();
    }
}
