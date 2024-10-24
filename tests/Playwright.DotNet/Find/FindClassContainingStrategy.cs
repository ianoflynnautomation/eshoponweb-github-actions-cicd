namespace Playwright.DotNet.Find;

public class FindClassContainingStrategy(string value) : FindCssStrategy($"[class*='{value}']")
{
    private string _value = value;

    public override string ToString()
    {
        return $"Class containing {_value}";
    }
}