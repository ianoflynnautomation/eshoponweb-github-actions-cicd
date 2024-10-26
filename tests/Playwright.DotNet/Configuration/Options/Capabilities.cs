using Microsoft.Extensions.Configuration;

namespace Playwright.DotNet.Configuration.Options;

// The reason for the Grid options (this class) to be a class, not a Dictionary<string, object>
// Is because Microsoft.Extensions.Configuration, when reading testFrameworkSettings, when it encounters a Dictionary,
// It will always try to parse it as Dictionary<string, string>, no matter what.
// And when this Dictionary contains another Dictionary or any collection at all
// It fails to parse it.
// Dictionary inside Dictionary happens when running tests in LambdaTest, because of the LT:Options
public class Capabilities
{
    public string BrowserName { get; set; }

    public string BrowserVersion { get; set; }

    public string PlatformName { get; set; }

    public string AcceptInsecureCerts { get; set; }

    public string PageLoadStrategy { get; set; }

    public string SetWindowRect { get; set; }

    public string UnhandledPromptBehaviour { get; set; }


    [ConfigurationKeyName("LT:Options")]
    public Dictionary<string, object> LtOptions { get; set; }


    [ConfigurationKeyName("browserstack.username")]
    public string BrowserStackUsername { get; set; }


    [ConfigurationKeyName("browserstack.accessKey")]
    public string BrowserStackAccessKey { get; set; }

    [ConfigurationKeyName("cloud.grid.user")]
    public string CloudGridUser { get; set; }

    [ConfigurationKeyName("cloud.grid.key")]
    public string CloudGridKey { get; set;}
}
