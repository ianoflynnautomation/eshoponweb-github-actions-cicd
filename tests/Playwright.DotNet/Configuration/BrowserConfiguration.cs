
using System.Drawing;
using Playwright.DotNet.Enums;

namespace Playwright.DotNet.Configuration;

public class BrowserConfiguration
{
    public BrowserTypes BrowserType { get; set; } = BrowserTypes.Chromium;
    public Size Size { get; set; }
    public string ClassFullName { get; set; }
    public dynamic PlaywrightOptions { get; set; }
    public dynamic ContextOptions { get; set; }
    public ExecutionType ExecutionType { get; set; } = ExecutionType.Regular;
    public Dictionary<string, object> GridOptions { get; set; }

    public bool Equals(BrowserConfiguration other) => ExecutionType.Equals(other?.ExecutionType) &&
                                                      BrowserType.Equals(other?.BrowserType) &&
                                                      Size.Equals(other?.Size) &&
                                                      GridOptions.SequenceEqual(other?.GridOptions);

    public override bool Equals(object obj)
    {
        if (obj is not BrowserConfiguration) return false;

        var browserConfiguration = (BrowserConfiguration)obj;

        return ExecutionType.Equals(browserConfiguration?.ExecutionType) &&
            BrowserType.Equals(browserConfiguration?.BrowserType) &&
            Size.Equals(browserConfiguration?.Size) &&
            GridOptions.Equals(browserConfiguration?.GridOptions);
    }

    public override int GetHashCode()
    {
        return ExecutionType.GetHashCode() +
                BrowserType.GetHashCode() +
                Size.GetHashCode() +
                GridOptions.GetHashCode();
    }
}
