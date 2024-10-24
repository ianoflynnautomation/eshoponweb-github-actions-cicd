
using Microsoft.Playwright;
using Playwright.DotNet.Find;

namespace Playwright.DotNet.Components;

/// <summary>
/// Represents a select dropdown on the page
/// </summary>
public class Select : Component
{

    public override Type ComponentType => GetType();

    public virtual void Hover()
    {
        Hover();
    }

    public virtual ComponentsList<Option> GetAllOptions()
    {
        return this.CreateAllByXpath<Option>(".//option");
    }

        public virtual void SelectByText(string text)
    {

        InternalSelect(new SelectOptionValue() { Label = text });

    }

        public virtual void SelectByValue(string value)
    {

        InternalSelect(new SelectOptionValue() { Value = value });

    }

    public virtual bool IsDisabled => GetDisabledAttribute();

    public virtual bool IsRequired => GetRequiredAttribute();

    public virtual bool IsReadonly => GetReadonlyAttribute();

     private void InternalSelect(SelectOptionValue option)
    {
        try
        {
            var optionValue = WrappedElement.SelectOption(option)[0];
            if (string.IsNullOrEmpty(optionValue)) throw new ArgumentException("Returning option value was empty, something went wrong during selection.");
        }

        catch
        {
            throw;
        }
    }

}