using Microsoft.Extensions.Configuration;

namespace Playwright.DotNet.Configuration;

/// <summary>
/// Provides a single instance of the configuration root.
/// </summary>
public static class ConfigurationRootInstance
{
    private static readonly Lazy<IConfigurationRoot> _configurationValue = new(GetTestConfiguration());

    public static IConfigurationRoot TestConfiguration => _configurationValue.Value;

    /// <summary>
    /// Gets a section from the configuration.
    /// </summary>
    /// <typeparam name="TSection">Type of Settings</typeparam>
    /// <param name="sectionName">Section name in the conguration. For example, WebSettings</param>
    /// <returns>The sections of the configuration</returns>
    public static TSection GetSection<TSection>(string sectionName) where TSection : class
    {
        return TestConfiguration.GetSection(sectionName).Get<TSection>() ?? throw new InvalidOperationException($"Section {sectionName} not found in configuration.");
    }

    private static IConfigurationRoot GetTestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", optional: true, reloadOnChange: true);

        return builder.Build();
    }
}